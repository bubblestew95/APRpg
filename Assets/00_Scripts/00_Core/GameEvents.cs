using UnityEngine;


/// <summary>
/// 게임에서 사용할 모든 이벤트 구조체를 이 곳에 정의합니다.
/// </summary>
public static class GameEvents
{
    /// <summary>
    /// 플레이어 이동 입력 이벤트.
    /// </summary>
    public struct PlayerMoveEvent
    {
        public Vector2 moveDirection;
    }

    /// <summary>
    /// 플레이어 공격 입력 이벤트.
    /// </summary>
    public struct PlayerAttackEvent
    {

    }
}
