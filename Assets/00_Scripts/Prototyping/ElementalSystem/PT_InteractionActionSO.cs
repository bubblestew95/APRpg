using UnityEngine;

// 모든 인터랙션 액션의 기반이 될 추상 클래스입니다.
// 새로운 액션을 만들려면 이 클래스를 상속받으세요.
public abstract class PT_InteractionActionSO : ScriptableObject
{
    // position: 상호작용이 일어난 위치
    // self: 이 액션을 트리거한 오브젝트
    // other: 액션의 대상이 되는 오브젝트
    public abstract void Execute(Vector3 position, GameObject self, GameObject other);
}
