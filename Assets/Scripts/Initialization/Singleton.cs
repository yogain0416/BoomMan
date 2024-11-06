using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T _instance;

    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                // 해당 컴포넌트를 가지고 있는 게임 오브젝트를 찾아서 반환한다.
                _instance = (T)FindAnyObjectByType(typeof(T));

                if (_instance == null) // 인스턴스를 찾지 못한 경우
                {
                    // 새로운 게임 오브젝트를 생성하여 해당 컴포넌트를 추가한다.
                    GameObject obj = new GameObject(typeof(T).Name, typeof(T));
                    // 생성된 게임 오브젝트에서 해당 컴포넌트를 instance에 저장한다.
                    _instance = obj.GetComponent<T>();
                }
            }

            return _instance;
        }
    }

    protected virtual void Awake()
    {
        if (transform.parent != null && transform.root != null) // 해당 오브젝트가 자식 오브젝트라면
        {
            DontDestroyOnLoad(this.transform.root.gameObject); // 부모 오브젝트를 DontDestroyOnLoad 처리
        }
        else
        {
            DontDestroyOnLoad(this.gameObject); // 해당 오브젝트가 최 상위 오브젝트라면 자신을 DontDestroyOnLoad 처리
        }
    }
}