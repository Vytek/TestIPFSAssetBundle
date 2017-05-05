using UnityEngine;
using UnityEditor;
using System.Collections;

namespace Vire
{
    public class SizeInfo : EditorWindow
    {
        // Add menu item named "Size Info" to the Window menu
        [MenuItem("Window/Size Info")]
        public static void ShowWindow()
        {
            //Show existing window instance. If one doesn't exist, make one.
            EditorWindow editor = EditorWindow.GetWindow(typeof(SizeInfo));
            editor.titleContent.text = "Size Info";
        }

        public Bounds getBounds(GameObject gameObject)
        {
            Bounds objectsBounds = new Bounds(gameObject.transform.position, Vector3.zero);

            Renderer renderer = gameObject.GetComponent<Renderer>();
            if (renderer)
                objectsBounds.Encapsulate(renderer.bounds);

            Renderer[] renderers = gameObject.GetComponentsInChildren<Renderer>();
            foreach (Renderer render in renderers)
                objectsBounds.Encapsulate(render.bounds);

            return objectsBounds;
        }

        void OnGUI()
        {
            if (Selection.activeObject != null && Selection.activeObject.GetType() == typeof(GameObject))
            {
                GameObject thisObject = (GameObject)Selection.activeObject;
                if (!thisObject)
                {
                    return;
                }

                Bounds objectBounds = getBounds(thisObject);
                Vector3 size = objectBounds.size;

                string numberFormat = "0.000";
                EditorGUILayout.LabelField("x", "" + size.x.ToString(numberFormat));
                EditorGUILayout.LabelField("y", "" + size.y.ToString(numberFormat));
                EditorGUILayout.LabelField("z", "" + size.z.ToString(numberFormat));
            }
        }

        void OnInspectorUpdate()
        {
            Repaint();
        }
    }
}