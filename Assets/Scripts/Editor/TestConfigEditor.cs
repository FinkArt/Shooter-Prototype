// using System.Linq;
// using UnityEditor;
// using UnityEngine;
// using UnityEngine.UI;
// using Image = UnityEngine.UIElements.Image;
//
// [CustomEditor(typeof(HealthConfig))]
// public class TestConfigEditor : Editor
// {
//     public GameObject x;
//     public override void OnInspectorGUI()
//     {
//         var config = (HealthConfig) target; //получаем доступ ко всем существующим элементам файла TestConfig
//         //DrawDefaultInspector(); // в инспекторе выводим все элементы TestConfig
//         GUILayout.Space(20);
//         if(!config.MagicIsDone)
//             config.SomeField = EditorGUILayout.IntField("Some description", config.SomeField);
//         if (GUILayout.Button("+"))
//         {
//             
//             config.Enemies.Add(new Enemies());
//     
//         }
//         foreach (var enemy in config.Enemies.ToList())
//         {
//             EditorGUILayout.BeginVertical("Box");
//             if (GUILayout.Button("-"))
//             {
//             
//                 config.Enemies.Remove(enemy);
//     
//             }
//             enemy.EnemiesName = EditorGUILayout.TextField("Some description", enemy.EnemiesName);
//             EditorGUILayout.EndVertical();
//             EditorGUILayout.Space(5f);
//         }
//         if (config.MagicIsDone)
//         {
//             EditorGUILayout.HelpBox("You are cool one", MessageType.Info);
//             GUI.color = Color.gray;
//             if (GUILayout.Button("Do some magic"))
//             {
//                 
//             }
//             GUI.color = Color.white;
//         }
//         else
//         {
//             EditorGUILayout.HelpBox("Click it!", MessageType.Warning);
//             GUI.color = Color.green;
//             if (GUILayout.Button("Do some magic"))
//             {
//                 config.SomeMagicHere();
//             }
//             GUI.color = Color.white;
//         }
//         GUILayout.Space(10);
//         EditorGUILayout.HelpBox("Example message", MessageType.Error);
//         //GUI.color = new Color(140, 140, 140);
//         GUI.color = Color.cyan;
//         if (GUILayout.Button("Play me"))
//         {
//             Debug.Log("Game started!");
//         }
//
//     }
// }
