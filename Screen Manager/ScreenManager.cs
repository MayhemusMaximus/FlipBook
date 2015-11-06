using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FlipBook
{
    public static class ScreenManager
    {
        #region Variables

        public static Dictionary<String, BaseScreen> ActiveScreens = new Dictionary<String, BaseScreen>();
        public static Dictionary<String, BaseScreen> NewScreens = new Dictionary<String, BaseScreen>();

        #endregion

        #region Methods

        public static void Load()
        {
            foreach (KeyValuePair<String, BaseScreen> screen in ActiveScreens)
            {
                screen.Value.Load();
            }
        }

        public static void Unload()
        {
            foreach (KeyValuePair<String, BaseScreen> screen in ActiveScreens)
            {
                screen.Value.Unload();
            }
        }

        public static void Update()
        {
            AddScreens();

            foreach(KeyValuePair<String,BaseScreen> screen in ActiveScreens)
            {
                screen.Value.Update();
            }
        }

        public static void Draw()
        {
            foreach (KeyValuePair<String, BaseScreen> screen in ActiveScreens)
            {
                screen.Value.Draw();
            }
        }

        public static void AddScreen(BaseScreen screen)
        {
            NewScreens.Add(screen.Name, screen);
        }

        private static void AddScreens()
        {
            if (NewScreens != null)
            {
                foreach (KeyValuePair<string, BaseScreen> screen in NewScreens)
                {
                    ActiveScreens.Add(screen.Key, screen.Value);
                }
                NewScreens = null;
            }
        }

        #endregion
    }
}
