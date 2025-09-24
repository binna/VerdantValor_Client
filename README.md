# Verdant Valor (2025.07.01 ~ 2025.08.29)

![image](https://github.com/user-attachments/assets/9bacff69-a94f-4ae6-a570-01383085a5fc)


<br><br>


## 목차

1. [게임소개](#게임소개) 
2. [Scene Flow](#scene-flow)
3. [주요기능](#주요기능)
4. [Player Balance](#player-balance)
5. [Monster Balance](#monster-balance)
6. [기술스택](#기술스택)
7. [기술적 구현](#기술적-구현)
8. [이슈](#이슈)
9. [아트 리소스](#아트-리소스)
10. [사운드 리소스](#사운드-리소스)


<br><br>


## 게임소개

> 2D 전투 RPG – 신비한 숲에서 펼쳐지는 기사의 모험

<br>

- **프로젝트 이름 의미**  
  - Verdant = 푸른 숲, 생명력  
  - Valor = 용기, 전사의 용맹
  
  Verdant Valor는 자연 속에서 싸우는 기사들의 모험을 상징하는 이름입니다.

<br>

- **조작법**
  - 공격: `Alt`  
  - 점프: `Ctrl`  
  - 이동: `←`, `→` (방향키)  
  - 숙이기: `↓` (방향키)
 
<br>

- [**📽️ 시연 영상 보기**](https://www.youtube.com/watch?v=jal_0tfmpjY)

  
<br><br>


## Scene Flow
<img width="903" height="707" alt="image" src="https://github.com/user-attachments/assets/4673a9d5-ad31-4952-bdf3-205f54eb477a" />


<br><br>


## 주요기능
1.  **조이스틱 방식 (*Joystick Control Style*)**  
   마을에서는 조이스틱으로 이동을 수행

2. **키보드 방식 (*Keyboard Control Style*)**  
   전투 공간에서는 키보드 조작으로 수행

3. **실시간 전투 시스템 (*Real-Time Combat System*)**  
   회피, 공격, 피격 애니메이션을 포함하며, 시간 정지 없이 진행되는 액션 중심 전투

4. **적 AI 시스템 (*Enemy AI System*)**  
   FSM 기반 상태 로직 적용  
   시야각 및 거리 조건을 만족하면 공격 수행

5. **마을 복귀 시스템 (*Return to Village*)**  
   전투 중 사망 시 자동으로 마을 위치로 복귀

6. **인벤토리 (*Inventory*)**  
   간단한 인벤토리 UI 제공  
   아이템 획득 및 소비 가능

8. **유저 정보 저장 (*User Information Save*)**  
   유저의 레벨, 소지한 재화 정보 저장

9. **타일맵 기반 맵 디자인 (*Tilemap-Based Map Design*)**  
   Tilemap 시스템을 활용한 유연한 공간 설계

<br><br>

## Player Balance

| Level | HP  | ATK | 누적 경험치 (EXP) |
|-------|-----|-----|------------|
| 1     | 100 | 3.0 | 100        |
| 2     | 150 | 3.5 | 300        |
| 3     | 220 | 4.2 | 700        |
| 4     | 300 | 5.0 | 1200       |
| 5     | 400 | 6.0 | 2000       |

<br><br>

## Monster Balance

| 몬스터       | HP   | Speed | Attack Time | Damage | Trace Distance | Attack Distance | 특징                  | 획득 경험치 (EXP) |
|--------------|------|-------|-------------|--------|----------------|-----------------|-----------------------|----|
| FlyingEye    | 10f  | 3f    | 1.5f        | 1f     | 7f             | 1.5f            | 빠르고 자주 공격하지만 약함 | 5  |
| Goblin       | 20f  | 2f    | 2f          | 3f     | 5f             | 1.2f            | 밸런스형, 다수일 때 위험   | 12 |
| Mushroom     | 35f  | 0.8f  | 3f          | 2f     | 4f             | 1f              | 느리고 단단한 탱커   | 20 |
| Skeleton     | 15f  | 1.5f  | 1.8f        | 4f     | 6f             | 1.3f            | 공격력 높지만 체력 약함   | 15 |

<br><br>

## 기술스택

| 항목 | 내용 |
|------|------|
| Engine | Unity 6000.0.46f1 |
| Language | C# |
| IDE | JetBrains Rider |

<br><br>

## 기술적 구현

<details>
<summary>왕복 이동 플랫폼 (Move Platform)</summary>
  
  - Mathf.Cos(`theta`) 기반의 왕복 이동 로직으로 수평/수직 플랫폼 이동 구현
  - `speed`와 `power` 값으로 속도와 이동 범위를 손쉽게 조정 가능하도록 구현
  - `Time.deltaTime` 기반의 계산으로 프레임 환경과 무관한 부드러운 이동 지원
  - 단순한 구조로 유지 보수와 커스터마이징에 용이하도록 구현
  
</details>

<details>
<summary>점프 플랫폼 (Push Platform)</summary>
  
  - 유저가 플랫폼 트리거에 진입 시 위쪽에 힘을 즉시 가해 점프 동작 구현
  - Animator 트리거와 연동해 플랫폼 애니메이션을 자연스럽게 재생
  - `pushPower`를 조절해 다양한 강도의 점프 연출 가능
  
</details>

<details>
<summary>사다리 (Ladder)</summary>

  - 유저가 사다리 범위에 진입 시 중력을 0으로 전환해 캐릭터가 자연스럽게 사다리에 고정되도록 구현
  - 상/하 입력에 따라 Y축 이동을 지원하여 부드럽고 자유로운 상하 이동 가능
  - 사다리 범위를 벗어나면 중력을 복원해 정상적인 이동 상태로 매끄럽게 전환
  - 단순한 상태 플래그(`_isLadder`)로 캐릭터 상태를 관리하여 유지 보수와 확장이 용이

</details>

<details>
<summary>동굴 연출 시스템</summary>
  
  - 유저가 동굴 트리거에 진입하면 카메라 크기를 조절해 자연스러운 줌인/줌아웃 연출 구현
  - 동굴 진입 시 Global Light 비활성화하고 유저 주변에만 빛을 비춰 스포트라이트 효과로 몰입감 강화
  - 동굴 트리거 기반으로 진입/이탈을 감지해 조명, 카메라, 캐릭터 상태를 원복하여 부드러운 전환 제공  
    
</details>

<br><br>

## 이슈

<details>
<summary>씬 이동 시 데이터 초기화 문제</summary>
  
  - **문제 (Issue)**
    - 씬 전환 시 유저 상태나 게임 데이터 같은 핵심 지속 데이터가 초기화
  
  - **해결 (Solution)**
    - 공통 데이터는 `static`으로 관리해 씬 전환과 무관하게 유지
    - 각 씬에 `@InitSetting` 오브젝트를 두어 HUD, BGM, UI 등 씬 전환 시 필요한 요소만 안전하게 재초기화

  - **성과 (Outcome)**
    - 씬 이동 후에도 핵심 데이터가 안정적으로 유지
    - 중복 로드나 데이터 손실을 방지하여 사용자 경험의 안정성을 확보

</details>

<details>
<summary>오브젝트 풀링 도입</summary>

  - **문제 (Issue)**
    - 몬스터를 Hierarchy에 수동으로 배치/생성해야 하는 비효율적인 관리 방식
    - 몬스터 수를 랜덤하게 제어하지 못해 게임 몰입감과 재미가 제한
  
  - **해결 (Solution)**
    - 오브젝트 풀링을 적용해 몬스터 타입별 최대 수를 제한하고, 최소 ~ 최대 범위 내에서 랜덤한 수량을 프리팹을 이용해 사전 생성
    - 코루틴으로 랜덤 시간 간격에 맞춰 비활성 개체를 활성화(스폰)하여 동적인 전투 흐름 구현

  - **성과 (Outcome)**
    - 수동 생성 작업을 자동화해 유지보수 효율 대폭 향상
    - 사전 생성과 재사용으로 Instantiate과 Destroy의 빈도 감소 → 프레임 안정성 확보
    - 스폰 간격 및 수량의 랜덤화로 전투의 변주와 몰입감 강화
    - 활성/비활성 전환 기반 관리로 GC 부담 최소화

</details>

<details>
<summary>아이템 드랍 및 공격력 강화에 타이머 기능 추가</summary>

- **문제 (Issue)**  
  - 몬스터가 사망했을 때 아이템이 즉시 드랍되어, 몬스터 사망 사운드와 아이템 드랍 사운드가 겹쳐 연출이 부자연스러움
  - 강화 아이템 사용 시 공격력 증가 효과가 무기한 유지되어 전투 밸런스가 무너지고 게임의 난이도 조절이 불가능

- **해결 (Solution)**  
  - 코루틴을 활용해 일정 시간 후 아이템이 드랍되도록 로직을 개선해 사운드 연출을 자연스럽게 조정
  - 강화 효과의 지속 시간을 코루틴 타이머로 관리하여, 일정 시간이 지나면 자동으로 효과가 해제되도록 구현

- **성과 (Outcome)**
  - 드랍 타이밍이 플레이 흐름과 어울리도록 개선되어 게임 몰입감 향상
  - 강화 효과가 일정 시간만 유지되도록 하여 전투 밸런스를 유지하고 게임 난이도를 안정적으로 조절 가능

</details>

<details>
<summary>코루틴 안정화</summary>
  
  - **문제 (Issue)**
    - 드랍 아이템의 타이머가 해당 아이템 객체에 종속되어 있어, 아이템이 삭제되면 타이머도 함께 종료되는 문제가 발생
    - 몬스터 사망 후 아이템 드랍 처리 시 실행되는 코루틴도 몬스터 객체가 비활성화되면 강제로 종료될 위험 존재
    
  - **해결 (Solution)**
    - 삭제되지 않는 `@SoundManager`에서 코루틴을 실행하도록 구조 변경
      
  - **성과 (Outcome)**
    - 타이머 중단 버그가 완전히 해소되어 게임 안정성 대폭 향상
    - 몬스터 드랍 처리 중 발생할 수 있는 에러 위험도 제거되어 전체 시스템 안정성 향상
      
</details>

<details>
<summary>하드코딩 데이터 구조를 JSON 기반으로 전환</summary>

  - **문제 (Issue)**
    - 몬스터, 아이템, 경험치 등 게임 데이터를 코드 안에 직접 작성해 관리
    - 데이터 변경 시마다 재빌드가 필요해 유지보수가 불편
  
  - **해결 (Solution)**
    - 데이터를 JSON 파일로 분리하고 `JsonUtility`를 이용하여 런타임에 데이터를 읽고 쓸 수 있도록 개선

  - **성과 (Outcome)**
    - 밸런스나 데이터 변경을 파일만 수정해도 즉시 반영 가능해 운영 효율 상승
    - 코드 수정 없이 운영할 수 있어 유지보수 효율 대폭 향상
    - 빌드 환경에서도 안정적으로 동작해 실무와 비슷한 데이터 관리 경험 확보

</details>


<br><br>


## 아트 리소스

- `Art/fonts/BMJUA` → [배민 주아체](https://noonnu.cc/font_page/53)
  
- `Art/Images/BattleProp` → ChatGPT
  
- `Art/Images/LoadingBackground` → [Asset Store](https://assetstore.unity.com/packages/2d/environments/background-for-mobile-games-portrait-2d-art-246460)

- `Art/Images/Portal` → [Asset Store](https://assetstore.unity.com/packages/vfx/particles/free-quick-effects-vol-1-304424)
 
- `Art/Tilemap`  
  - Town → [itch.io](https://otterisk.itch.io/hana-caraka-topdown-tileset)
  - Town House → [itch.io](https://anokolisa.itch.io/free-pixel-art-asset-pack-topdown-tileset-rpg-16x16-sprites)
  - Town Npc → [itch.io](https://schwarnhild.itch.io/summer-village)
  - Adventure → [Asset Store](https://assetstore.unity.com/packages/2d/environments/2d-pixel-art-platformer-biome-american-forest-255694)

- `Art/UI/Icon` → ChatGPT
 
- `Art/UI/Joystick` → [Asset Store](https://assetstore.unity.com/packages/tools/input-management/simple-input-system-113033)

- `Resources/Images/Item` → [Asset Store](https://assetstore.unity.com/packages/2d/gui/icons/2d-pixel-item-asset-pack-99645)
 
- `캐릭터` → [itch.io](https://aamatniekss.itch.io/fantasy-knight-free-pixelart-animated-character)
 
- `몬스터` → [Asset Store](https://assetstore.unity.com/packages/2d/characters/monsters-creatures-fantasy-167949)


<br><br>


## 사운드 리소스

- [Pixabay](https://pixabay.com)
  - [IntroBGM](https://pixabay.com/music/solo-instruments-magic-forest-318165/) → 인트로 배경 음악
    
  - [AdventureBGM](https://pixabay.com/music/upbeat-black-box-hypocrisy-112160/) → 전투 배경 음악
    
  - [Portal](https://pixabay.com/sound-effects/magic-teleport-whoosh-352764/) → 포탈 이동 효과음
    
  - [Gameover](https://pixabay.com/sound-effects/8bit-lose-life-sound-wav-97245/) → 캐릭터 사망 효과음
  
  - [MonsterDie](https://pixabay.com/sound-effects/super-deep-growl-86749/) → 몬스터 사망 효과음
    
  - [ItemPickup](https://pixabay.com/sound-effects/item-pickup-37089/) → 아이템 획득 효과음
    
  - [LevelUp](https://pixabay.com/sound-effects/game-level-complete-143022/) → 레벨업 효과음
  
- Asset Store
  - [TownBGM](https://assetstore.unity.com/packages/audio/music/electronic/8-bit-rpg-adventure-music-pack-184726), Track: 04 Overworld → 마을 배경 음악  

<br><br>

---

<div align="center">

   ⭐ **Thanks for taking a look at Meow Jump Game!** ⭐

   📌 **본 프로젝트는 개인 포트폴리오 용도로 제작되었습니다**  
   저작권 및 기타 문제되는 부분이 있을 경우  
   `every5116@naver.com` 으로 연락 주시면 신속히 대응하겠습니다
   
</div>
