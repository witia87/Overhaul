using Assets.Flux;
using UnityEngine;

namespace Assets
{
    public class GameMechanics
    {
        private static bool _isInitialized;
        private static GameMechanics _instance;

        private Dispatcher _dispatcher;
        private Random _random;
        private IStores _stores;

        public static IDispatcher Dispatcher
        {
            get
            {
                if (!_isInitialized)
                {
                    Initialize();
                }
                return _instance._dispatcher;
            }
        }

        public static IStores Stores
        {
            get
            {
                if (!_isInitialized)
                {
                    Initialize();
                }
                return _instance._stores;
            }
        }

        public static Random Random
        {
            get
            {
                if (!_isInitialized)
                {
                    Initialize();
                }
                return _instance._random;
            }
        }

        private static void Initialize()
        {
            _instance = new GameMechanics();

            _instance._dispatcher = new Dispatcher();
            _instance._stores = new Stores(_instance._dispatcher);
            _instance._random = new Random();

            _isInitialized = true;
        }
    }
}