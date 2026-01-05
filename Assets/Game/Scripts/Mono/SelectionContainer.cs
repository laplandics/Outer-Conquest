using UnityEngine;

public class SelectionContainer : MonoBehaviour
{
    [SerializeField] private RectTransform containerTransform;
    public RectTransform ContainerTransform => containerTransform;
}