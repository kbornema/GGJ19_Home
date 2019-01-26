using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace CustomTextureImport
{
    [CreateAssetMenu(menuName = "Misc/TextureImport/ImportSetting")]
    public class TextureImportSettings : ScriptableObject
    {
        [Header("Filter")]
        [SerializeField]
        private bool _isEnabled = true;
        [SerializeField]
        private string _fileContent = "";
        [SerializeField]
        private string _fileEnding = "";

        [Header("Settings")]
        [SerializeField]
        private string _packingTag = "";
        [SerializeField]
        private float _pixelsPerUnit = 32;
        [SerializeField]
        private TextureImporterType _textureType = TextureImporterType.Sprite;
        [SerializeField]
        private bool _mipMap = false;
        [SerializeField]
        private FilterMode _filterMode = FilterMode.Point;
        [SerializeField]
        private TextureImporterCompression _compression = TextureImporterCompression.Uncompressed;
        [SerializeField]
        private List<DefaultAsset> _folders = null;
        [SerializeField]
        private List<DefaultAsset> _foldersToIgnore = null;

        public bool CanImport(string assetPath, TextureImporter textureImporter)
        {
            if (!_isEnabled)
                return false;

            if (!assetPath.EndsWith(_fileEnding, System.StringComparison.OrdinalIgnoreCase))
                return false;

            if(_fileContent.Length > 0 && !assetPath.Contains(_fileContent))
                return false;

            for (int i = _foldersToIgnore.Count - 1; i >= 0; i--)
            {
                if (_foldersToIgnore[i] == null)
                {
                    _foldersToIgnore.RemoveAt(i);
                    continue;
                }

                var folderPath = AssetDatabase.GetAssetPath(_foldersToIgnore[i]);

                if (assetPath.StartsWith(folderPath))
                    return false;
            }

            for (int i = _folders.Count - 1; i >= 0; i--)
            {
                if(_folders[i] == null)
                {
                    _folders.RemoveAt(i);
                    continue;
                }

                var folderPath = AssetDatabase.GetAssetPath(_folders[i]);

                if (assetPath.StartsWith(folderPath))
                    return true;
            }

            return false;
        }

        public bool Import(string assetPath, TextureImporter textureImporter)
        {
            textureImporter.textureType = _textureType;
            textureImporter.spritePackingTag = _packingTag;
            textureImporter.mipmapEnabled = _mipMap;
            textureImporter.filterMode = _filterMode;
            textureImporter.spritePixelsPerUnit = _pixelsPerUnit;
            textureImporter.textureCompression = _compression;
            
            return true;
        }
    }
}