using UnityEngine;
using System.Collections;

public class WallGlowTrigger : MonoBehaviour
{
    public Material changedMaterial;
    public int pointsToAdd = 5; 

    private Material originalMaterial;
    private MeshRenderer[] wallMeshRenderers;
    private bool materialChanged = false;

    void Start()
    {
        wallMeshRenderers = GetComponentsInChildren<MeshRenderer>();
        originalMaterial = wallMeshRenderers[0].material;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !materialChanged)
        {
            StartCoroutine(ChangeWallMaterialForDuration());
        }
    }

    private IEnumerator ChangeWallMaterialForDuration()
    {
        foreach (MeshRenderer quadRenderer in wallMeshRenderers)
        {
            quadRenderer.material = changedMaterial;
        }

        materialChanged = true;

        yield return new WaitForSeconds(3f);

        RevertWallMaterial();
    }

    void RevertWallMaterial()
    {
        foreach (MeshRenderer quadRenderer in wallMeshRenderers)
        {
            quadRenderer.material = originalMaterial;
        }

        materialChanged = false;
    }
}

