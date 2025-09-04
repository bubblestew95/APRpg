# 프로젝트 설명

이 프로젝트는 3D 쿼터뷰 액션 RPG 게임 프로젝트입니다.
스킬, 속성의 조합에 따라 액션과 퍼즐을 풀어가며 재미를 얻는 게임입니다.

# 스크립트 폴더 구조

이 프로젝트는 아래의 규칙에 따라 스크립트 폴더를 관리합니다.

## `스크립트 폴더 링크`
*   **역할**: 해당 폴더 내의 스크립트의 역할
*   **키워드**: 스크립트 클래스 이름에 들어갈 키워드
*   **예시**: 이름 예시

## `Assets/00_Scripts/Prototyping`
*   **역할**: 프로토 타이핑 시 간단한 기능들을 빠르게 구현하는 스크립트. 
            클래스 이름의 맨 앞에 프로토 타이핑 용 스크립트라는 뜻으로 `PT_` 가 붙어야 한다.
*   **키워드**: `PT_`
*   **예시**: `PT_PlayerMove.cs`

## `Assets/00_Scripts/Core`
*   **역할**: 게임의 핵심 로직, 관리자(Manager) 클래스, 싱글톤.
*   **키워드**: `Manager`, `Core`, `System`, `Service`, `Singleton`, `Game`
*   **예시**: `GameManager.cs`, `Health.cs`, `IDamageable.cs`

## `Assets/00_Scripts/Player`
*   **역할**: 플레이어 캐릭터의 조작, 상태, 인벤토리 등 플레이어와 직접적으로 관련된 모든 스크립트. `MonoBehaviour`를 상속하는 컴포넌트와 상속하지 않는 순수 로직 클래스를 모두 포함.
*   **키워드**: `Player`, `Character`, `Controller`, `Input`, `Movement`
*   **예시**: `PlayerManager.cs` (MonoBehaviour), `PlayerMovement.cs` (Logic)

## `Assets/00_Scripts/Enemy`
*   **역할**: 적 캐릭터의 AI, 행동 패턴, 상태 등 모든 적 관련 스크립트. `MonoBehaviour`를 상속하는 컴포넌트와 상속하지 않는 순수 로직 클래스를 모두 포함.
*   **키워드**: `Enemy`, `Monster`, `AI`, `Boss`
*   **예시**: `EnemyAI.cs` (MonoBehaviour), `SlimeMovement.cs` (Logic), `BossPattern.cs` (Logic)

## `Assets/00_Scripts/UI`
*   **역할**: UI 요소(버튼, 슬라이더, 텍스트 등)의 동작, 상호작용, 데이터 표시를 담당하는 스크립트.
*   **키워드**: `UI`, `Button`, `Menu`, `HUD`, `Panel`, `Window`
*   **예시**: `MainMenu.cs`, `HealthBarUI.cs`, `SettingsWindow.cs`

## `Assets/00_Scripts/Utils`
*   **역할**: 특정 기능에 종속되지 않고 여러 곳에서 사용될 수 있는 유틸리티, 헬퍼, 확장 메소드 등.
*   **키워드**: `Util`, `Helper`, `Extension`, `Common`
*   **예시**: `TransformExtensions.cs`, `JsonHelper.cs`

# C# 스크립트 스타일 가이드

이 프로젝트의 C# 스크립트는 다음 스타일 가이드를 따릅니다.

## #region 사용 규칙

스크립트의 가독성을 높이기 위해, 클래스 내부의 코드를 기능별로 그룹화하는 `#region` 전처리문을 적극적으로 사용합니다.
아래 순서를 기준으로 작성하는 것을 원칙으로 합니다.

### 1. Public, Serialized Fields
*   **역할**: Inspector 창에 노출되는 `public` 변수나 `[SerializeField]` 어트리뷰트가 붙은 변수들을 정의합니다.

### 2. Private Fields
*   **역할**: Inspector 창에 노출되지 않는 순수 `private` 멤버 변수들을 정의합니다.

### 3. Properties
*   **역할**: `public` 또는 `private` 프로퍼티(`{ get; set; }`)들을 이곳에 정의합니다.

### 4. Public Methods
*   **역할**: 클래스 외부에서 호출할 수 있는 모든 `public` 메소드를 정의합니다. 이 클래스의 핵심 기능(API)에 해당합니다.

### 5. Private Methods
*   **역할**: 클래스 내부에서만 사용되는 모든 `private` 헬퍼(Helper) 메소드를 정의합니다.

### 6. Unity Callbacks
*   **역할**: `Awake()`, `Start()`, `Update()`, `OnEnable()` 등 Unity 엔진이 호출하는 생명주기 메소드를 모아둡니다.

# C# 스크립트 기본 템플릿

아래 템플릿을 복사하여 새로운 C# 스크립트를 빠르게 생성할 수 있습니다.

```csharp

    #region Public, Serialized Fields
    #endregion

    #region Private Fields
    #endregion

    #region Properties
    #endregion

    #region Public Methods
    #endregion

    #region Private Methods
    #endregion

    #region Unity Callbacks

    #endregion
    
```