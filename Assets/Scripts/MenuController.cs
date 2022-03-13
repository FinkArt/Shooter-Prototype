using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    [SerializeField] private Button _playButton;
    [SerializeField] private Button _settingsButton;
    [SerializeField] private Button _developersButton;
    [SerializeField] private Button _exitButton;
    [SerializeField] private Button _shopButton;
    [Space]
    [SerializeField] private GameObject _mainMenuWindow;
    [SerializeField] private GameObject _settingsMenuWindow;
    [SerializeField] private GameObject _developersMenuWindow;
    [SerializeField] private GameObject _shopWindow;
    [Space] 
    [SerializeField] private Button[] _backButtons;
    
    

    private void Awake()
    {
        _playButton.onClick.AddListener(OnPlayButtonClicked);
        _developersButton.onClick.AddListener(OnDevelopersButtonClicked);
        _settingsButton.onClick.AddListener(OnSettingsButtonClicked);
        _exitButton.onClick.AddListener(OnExitButtonClicked);
        _shopButton.onClick.AddListener(OnShopButtonClick);
        foreach (var backButton in _backButtons)
        {
            if (backButton == null)
            {
                Debug.LogError("Элемент цикла не заполнен!");
                continue;
            }
            backButton.onClick.AddListener(OnBackButtonClick);
        }
    }

    #region UIButtonsCallbacks
    private void OnPlayButtonClicked()
    {
        Debug.LogWarning("Play button was <color=yellow>clicked</color>!");
    }
    
    private void OnSettingsButtonClicked()
    {
        _settingsMenuWindow.SetActive(true);
        _mainMenuWindow.SetActive(false);
        _developersMenuWindow.SetActive(false);
        _shopWindow.SetActive(false);
    }

    private void OnDevelopersButtonClicked()
    {
        _settingsMenuWindow.SetActive(false);
        _mainMenuWindow.SetActive(false);
        _developersMenuWindow.SetActive(true);
        _shopWindow.SetActive(false);
    }
    
    private void OnExitButtonClicked()
    {
        #if UNITY_EDITOR
        EditorApplication.isPlaying = false;
        #else
        Application.Quit();
        #endif
    }

    private void OnBackButtonClick()
    {
        _settingsMenuWindow.SetActive(false);
        _mainMenuWindow.SetActive(true);
        _developersMenuWindow.SetActive(false);
        _shopWindow.SetActive(false);
    }

    private void OnShopButtonClick()
    {
        _settingsMenuWindow.SetActive(false);
        _mainMenuWindow.SetActive(false);
        _developersMenuWindow.SetActive(false);
        _shopWindow.SetActive(true);
    }
    #endregion
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
