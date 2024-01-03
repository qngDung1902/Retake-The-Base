using UnityEngine;
using System.Collections.Generic;

namespace PopupSystem
{
    /// <summary>
    /// Singleton pattern.
    /// </summary>
    public class SingletonPopup<T> : BasePopup where T : BasePopup
    {
        protected static T _instance;

        /// <summary>
        /// Singleton using for Popup Group only!
        /// </summary>
        /// <value>The instance.</value>
        public static T Get
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<T>();
                    if (_instance == null) _instance = PopupManager.Get.CheckInstancePopupPrebab<T>();
                }

                return _instance;
            }
        }

        public override void Awake()
        {
            base.Awake();
        }
    }

}