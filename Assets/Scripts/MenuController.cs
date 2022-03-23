using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public enum MenuName
{
    None = 0,
    MainMenu = 1,
    Play = 2,
    Settings = 3,
    Developers = 4,
    Exit = 5
    
}

[System.Serializable]
public class MenuMode
{
    public MenuName MenuName;
    public string MenuTitle;
    public Text MenuButtonText;
    public Button ModeButton;
    public GameObject ModePanel;
    public Button BackButton;

    public void SetButtonTitle()
    {
        if(MenuButtonText == null)
            return;
        MenuButtonText.text = MenuTitle;
    }

    public void SetMenuPanelStatus(bool isVisible)
    {
        if(ModePanel == null)
            return;
        ModePanel.SetActive(isVisible); 
    }
}

public class MenuController : MonoBehaviour
{
    [SerializeField] private MenuMode[] _mainMenuModes;
    [Space]
    [SerializeField] private MenuName _currentMenuName;
    [SerializeField] private MenuName _startMenuName;

    private MenuMode GetMenuByName(MenuName menuName)
    {
        for (int i = 0; i < _mainMenuModes.Length; i++)
        {
            if (_mainMenuModes[i].MenuName == menuName)
            {
                return _mainMenuModes[i];
            }
        }
    
        return null;
    }
    //
    // private MenuMode GetMenuByName2(MenuName menuName)
    // {
    //     foreach (var menuMode in _mainMenuModes)
    //     {
    //         if (menuMode.MenuName == menuName)
    //         {
    //             return menuMode;
    //         }
    //     }
    //
    //     return null;
    // }
    //
    // private MenuMode GetMenuByName3(MenuName menuName)
    // {
    //     return  _mainMenuModes.FirstOrDefault(menu => menu.MenuName == menuName);
    // }
    
    //private MenuMode GetMenuByName(MenuName menuName) => _mainMenuModes.FirstOrDefault(menu => menu.MenuName == menuName);

    private void Awake()
    {
        foreach (var menuMode in _mainMenuModes)
        {
            menuMode.SetButtonTitle();
            if (menuMode.ModeButton != null)
            {
                menuMode.ModeButton.onClick.AddListener(() => SelectMenuMode(menuMode.MenuName));
            }

            if (menuMode.BackButton != null)
            {
                menuMode.BackButton.onClick.AddListener(() => SelectMenuMode(MenuName.MainMenu));
            }
            
        }
        
        SelectMenuMode(_startMenuName);
    }

    public void SelectMenuMode(MenuName menuNameToOpen)
    {
        if (_currentMenuName == menuNameToOpen)
            return;

        if (_currentMenuName != MenuName.None)
        {
            var previousMenu = GetMenuByName(_currentMenuName);
            if(previousMenu != null)
                previousMenu.SetMenuPanelStatus(false);
        }
        _currentMenuName = menuNameToOpen;
        var openedMenu = GetMenuByName(_currentMenuName);
        if(openedMenu != null)
            openedMenu.SetMenuPanelStatus(true);

        switch (menuNameToOpen)
        {
           case MenuName.Exit:
                #if UNITY_EDITOR
                    EditorApplication.isPlaying = false;
                #else
                    Application.Quit();  
                #endif
               break;
           case MenuName.Play:
               Debug.Log("Запуск игры");
               break;
        }
    }
}

#region TheoryOfC#
// public class MenuController : MonoBehaviour
// {
//     [SerializeField] private int _myValue;
//
//     public int MyValue => _myValue;
//
//     public int Test
//     {
//         get
//         {
//             return _myValue;
//         }
//
//     }
//
//     public int Test2 { get; private set; } = 10;
//
//     [SerializeField] private GameObject mainMenu;
//     [SerializeField] private GameObject settingsMenu;
//     [SerializeField] private GameObject autorsMenu;
//     [SerializeField] private GameObject shop;
//    
//     [SerializeField] private GameObject[] a2;
//     [SerializeField] private List<GameObject> b;
//
//     private int _myValue2 = 10;
//     public int MyValue2;
//     
//     private bool autorsMenuOpen = false;
//     private bool settingsMenuOpen = false;
//
//     /*
//      int 
//      bool
//     char 'a'
//     string ""
//     float 10.532f
//     double 10.52323232d
//     
//     int[]
//     List<
//     
//
//
//     Vector3
//     GameObject
//     Transform
//     Light
//     MeshRenderer
//     Collider
//     AudioSource
//     Toggle
//     Image
//
//
//      */ 
//
//     private int[] a;
//     private int a1;
//
//     [SerializeField] private Button[] _backButtons;
//
//
//     public class TestClass
//     {
//         public int Age;
//     }
//     
//     private void Awake()
//     {
//
//         int a = 10; //20
//         int b = 20; //20
//         int c = a; //10
//         a += 10;
//
//         var a1 = new TestClass(); //15 + 10 = 25
//         a1.Age = 15;
//         var b1 = new TestClass(); //25
//         b1.Age = 25;
//         var c1 = new TestClass(); //15 + 10 = 25
//         c1 = a1;
//         a1.Age += 10;
//         
//       
//
//         var array = new int[5];
//         array[1] = 2;
//
//         var arr2 = new int[]
//         {
//             1, 2, 3, 4, 5
//         };
//
//         int[] testArray = new int[10];
//         var testList = new List<int>();
//         testList.Add(10);
//         testList.Add(20); 
//         testList.Add(30);
//         testList.Remove(10); 
//         testList.RemoveAt(0); 
//         testList.Clear();
//
//         var list2 = new List<int>();
//
//
//         int myV = 10;
//         
//         Test2 = 10;
//
//         foreach (var backButton in _backButtons)
//         {
//             continue;
//         }
//
//         try
//         {
//             EnableCollider();
//         }
//         catch (Exception exception)
//         {
//             gameObject.AddComponent<BoxCollider>();
//             Debug.Log(exception.Message);
//         }
//         finally
//         {
//             EnableCollider();
//         }
//
//         void EnableCollider()
//         {
//             var col = gameObject.GetComponent<BoxCollider>();
//             col.enabled = true;
//         }
//
//         var maxValue = Max(b: 10, a:20);
//     }
//
//     private int Max(int a, int b = 10)
//     {
//         int maxValue;
//         if (a > b)
//         {
//             return maxValue = a;
//         }
//         else
//         {
//             return maxValue = b;
//         }
//         //return maxValue;
//     }
//
//     private void OnBackButtonClicked()
//     {
//         autorsMenu.SetActive(false);
//         settingsMenu.SetActive(false);
//         shop.SetActive(false);
//         mainMenu.SetActive(true);
//     }
//
//     public void StartButton()
//     {
//         Debug.Log("тест");
//     }
//     public void SettingsButton()
//     {
//         settingsMenu.SetActive(true);
//         mainMenu.SetActive(false);
//         settingsMenuOpen = true;
//     }
//
//     public void AutorsMenu()
//     {
//         autorsMenu.SetActive(true);
//         mainMenu.SetActive(false);
//         autorsMenuOpen = true;
//     }
//
//     public void ExitButton()
//     {
//         Debug.Log("���������� ���������");
//     }
//
//
// }
#endregion
