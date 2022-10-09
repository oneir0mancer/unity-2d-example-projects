using System;
using UnityEngine;

namespace ShadedSprite
{
    [ExecuteAlways]
    public class ShadowSpriteTexture : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _renderer;
        [SerializeField] private Texture2D _texture;

        private readonly int _shadowTexParam = Shader.PropertyToID("_ShadowTex");

        private void OnEnable()
        {
            _renderer.material.SetTexture(_shadowTexParam, _texture);
        }

        private void OnValidate()
        {
            if (_renderer == null) return;
            
            _renderer.sharedMaterial.SetTexture(_shadowTexParam, _texture);
        }
    }
}
