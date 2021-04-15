using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IsolateIsland.Runtime.Managers
{
    public interface IManagerInit
    {
        void OnInit();
    }

    public interface IManagerUpdate
    {
        void OnUpdate();
    }

    [DefaultExecutionOrder(-3000)]
    public class Managers : MonoBehaviour
    {
        private static Managers _instance = null;
        public static Managers Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<Managers>();

                    if (_instance == null)
                        _instance = new GameObject($"@{nameof(Managers)}").AddComponent<Managers>();
                }
                return _instance;
            }
        }

        private CombinationManager _combinationManager;
        public CombinationManager combinationManager
        {
            get
            {
                if (_combinationManager == null)
                {
                    _combinationManager = new CombinationManager();
                    InitManager(_combinationManager);
                }

                return _combinationManager;
            }
        }

        private InputManager _inputManager;
        public InputManager inputManager
        {
            get
            {
                if (_inputManager == null)
                {
                    _inputManager = new InputManager();
                    InitManager(_inputManager);
                }

                return _inputManager;
            }
        }

        private InventoryManager _inventoryManager;
        public InventoryManager inventoryManager
        {
            get
            {
                if (_inventoryManager == null)
                {
                    _inventoryManager = new InventoryManager();
                    InitManager(_inventoryManager);
                }
                return _inventoryManager;
            }
        }

        private void InitManager(IManagerInit manager) => manager.OnInit();
        private void UpdateManager(IManagerUpdate manager) => manager.OnUpdate();

        private void Update()
        {
            UpdateManager(inputManager);
        }
    }
}
