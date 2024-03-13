using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class DancingEnnemy : BaseEnemyComponent
{
    private PlayerComponent m_player;

    public float Speed;

    private Material m_material;

    private List<SkinnedMeshRenderer> MeshRenderers;

    public void Start()
    {
        m_player = GameObject.FindObjectOfType<PlayerComponent>();
        MeshRenderers = GetComponentsInChildren<SkinnedMeshRenderer>().ToList();
    }

    private float m_hue = 0;

    private void Update()
    {
        Vector3 directionToPlayer = m_player.transform.position - transform.position;
        directionToPlayer = directionToPlayer.normalized;

        transform.position += Time.deltaTime * Speed * directionToPlayer;

        // Rainbow color
        foreach (var mr in MeshRenderers)
        {
            mr.material.color = Color.HSVToRGB(m_hue, 1, 1);
        }

        m_hue += 0.1f * Time.deltaTime;
        if (m_hue > 1)
            m_hue = 0;
    }
}