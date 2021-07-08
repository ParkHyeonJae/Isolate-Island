using UnityEngine;

namespace IsolateIsland.Runtime.Managers
{
    public interface IManagerInit
    {
        void OnInit();
    }

    public abstract class MonoManagerInit : MonoBehaviour, IManagerInit
    {
        public abstract void OnInit();
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
        public CombinationManager Combination
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
        public InputManager Input
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
        public InventoryManager Inventory
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

        private StatManager _statManager;
        public StatManager statManager
        {
            get
            {
                if (_statManager == null)
                {
                    _statManager = new StatManager();
                    InitManager(_statManager);
                }
                return _statManager;
            }
        }

        private DIManager _diManager;
        public DIManager DI
        {
            get
            {
                if (_diManager == null)
                {
                    _diManager = new DIManager();
                    InitManager(_diManager);
                }
                return _diManager;
            }
        }

        private GameManager _gameManager;
        public GameManager GameManager
        {
            get
            {
                if (_gameManager == null)
                {
                    _gameManager = new GameManager();
                    InitManager(_gameManager);
                }
                return _gameManager;
            }
        }

        private EventManager _eventManager;
        public EventManager Event
        {
            get
            {
                if (_eventManager == null)
                {
                    _eventManager = new EventManager();
                    InitManager(_eventManager);
                }
                return _eventManager;
            }
        }

        private ResourceManager _resourceManager;
        public ResourceManager Resource
        {
            get
            {
                if (_resourceManager == null)
                {
                    _resourceManager = new ResourceManager();
                    InitManager(_resourceManager);
                }
                return _resourceManager;
            }
        }

        private PoolManager _poolManager;
        public PoolManager Pool
        {
            get
            {
                if (_poolManager == null)
                {
                    _poolManager = new PoolManager();
                    InitManager(_poolManager);
                }
                return _poolManager;
            }
        }
        private CoroutineManager _coroutineManager;
        public CoroutineManager Coroutine
        {
            get
            {
                if (_coroutineManager == null)
                {
                    MonoInitManager(out _coroutineManager);
                }
                return _coroutineManager;
            }
        }
        private UtilManager _utilManager;
        public UtilManager Util
        {
            get
            {
                if (_utilManager == null)
                {
                    _utilManager = new UtilManager();
                    InitManager(_utilManager);
                }
                return _utilManager;
            }
        }
        private CameraManager _cameraManager;
        public CameraManager Camera
        {
            get
            {
                if (_cameraManager == null)
                {
                    _cameraManager = new CameraManager();
                    InitManager(_cameraManager);
                }
                return _cameraManager;
            }
        }

        private SoundManager _soundManager;
        public SoundManager Sound
        {
            get
            {
                if (_soundManager == null)
                {
                    MonoInitManager(out _soundManager);
                }
                return _soundManager;
            }
        }

        private RenderManager _renderManager;
        public RenderManager Render
        {
            get
            {
                if (_renderManager == null)
                {
                    _renderManager = new RenderManager();
                    InitManager(_renderManager);
                }
                return _renderManager;
            }
        }

        private void InitManager<I>(I manager) where I : IManagerInit => manager.OnInit();
        private void MonoInitManager<I>(out I manager) where I : MonoManagerInit
        {
            var name = typeof(I).ToString();
            name = name.Substring(name.LastIndexOf(".") + 1);
            var routineObject = new GameObject($"@{name}");
            routineObject.transform.SetParent(Managers.Instance.transform);
            manager = routineObject.AddComponent<I>();
            InitManager(manager);
        }
        private void UpdateManager<I>(I manager) where I : IManagerUpdate => manager.OnUpdate();

        private void Update()
        {
            UpdateManager(Input);
            UpdateManager(GameManager);
            UpdateManager(Render);
        }
    }
}
