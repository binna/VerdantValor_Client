# 🌿⚔️ Verdant Valor 

![image](https://github.com/user-attachments/assets/9bacff69-a94f-4ae6-a570-01383085a5fc)

---

## 🌿 About the Title
**Verdant Valor** = 🌿 Verdant (푸르른 숲, 생명력) + 🛡 Valor (용기, 전사의 용맹)  
→ 자연 속에서 싸우는 기사들의 모험을 상징하는 이름

---

<br><br>

<p align="center">
  <b>⚔️ 2D Action RPG – A Knight’s Adventure Unfolding in a Mysterious Forest 🌲</b> <br>
  <i>2D 전투 RPG – 신비한 숲에서 펼쳐지는 기사의 모험</i>
</p>

<p align="center">
  <a href="https://www.youtube.com/watch?v=jal_0tfmpjY" target="_blank">
    🎥 <b>Watch Gameplay on YouTube</b>
  </a>
</p>

<br><br>

<hr>

## 📑 Table of Contents

1. 🎮 [Gameplay](#gameplay)
2. 🗺️ [Scene Flow](#scene-flow)
3. 🧭 [Main Feature](#main-feature)
4. 🧙 [Player Balance](#player-balance)
5. 👾 [Monster Balance](#monster-balance)
6. 🛠️ [Tech Stack](#tech-stack)
7. 🧪 [Technical](#technical)
8. 🐞 [Known Issues & Solutions](#known-issues--solutions)
9. 🎨 [Art Resources](#art-resources)
10. 🎵 [Sound Resources](#sound-resources)

<hr>

<br><br>

## Gameplay
- **A 2D RPG that begins in a peaceful village and unfolds into battles deep within a mysterious forest.**  
  _마을에서 여정을 시작해, 숲속 깊은 곳에서 펼쳐지는 전투를 담은 2D RPG_

- **조작법**
  - 공격: `Alt`
    
  - 점프: `Ctrl`
    
  - 이동: `←`, `→` (방향키)
    
  - 숙이기: `↓` (방향키)
  
<br><br>

## Scene Flow
<img width="903" height="707" alt="image" src="https://github.com/user-attachments/assets/4673a9d5-ad31-4952-bdf3-205f54eb477a" />

<br><br>

## Main Feature
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

## Tech Stack

| 항목 | 내용 |
|------|------|
| Engine | Unity 6000.0.46f1 |
| Language | C# |
| IDE | JetBrains Rider |

<br><br>

## Technical

<details>
<summary>왕복 이동 플랫폼 (Move Platform)</summary>
  
  - Mathf.Cos(theta) 기반의 왕복 이동 로직으로 수평/수직 플랫폼 이동 구현
  - speed와 power 값으로 속도와 이동 범위를 손쉽게 조정 가능하도록 구현
  - Time.deltaTime 기반의 계산으로 프레임 환경과 무관한 부드러운 이동 지원
  - 단순한 구조로 유지 보수와 커스터마이징에 용이하도록 구현
  
</details>

<details>
<summary>점프 플랫폼 (Push Platform)</summary>
  
  - 유저가 플랫폼 트리거에 진입 시 위쪽에 힘을 즉시 가해 점프 동작 구현
  - Animator 트리거와 연동해 플랫폼 애니메이션을 자연스럽게 재생
  - pushPower를 조절해 다양한 강도의 점프 연출 가능
  
</details>

<details>
<summary>사다리 (Ladder)</summary>

  - 유저가 사다리 범위에 진입 시 중력을 0으로 전환해 캐릭터가 자연스럽게 사다리에 고정되도록 구현
  - 상/하 입력에 따라 Y축 이동을 지원하여 부드럽고 자유로운 상하 이동 가능
  - 사다리 범위를 벗어나면 중력을 복원해 정상적인 이동 상태로 매끄럽게 전환
  - 단순한 상태 플래그(isLadder)로 캐릭터 상태를 관리하여 유지 보수와 확장이 용이

</details>

<details>
<summary>동굴 연출 시스템</summary>
  
  - 유저가 동굴 트리거에 진입하면 카메라 크기를 조절해 자연스러운 줌인/줌아웃 연출 구현
  - 동굴 진입 시 Global Light 비활성화하고 유저 주변에만 빛을 비춰 스포트라이트 효과로 몰입감 강화
  - 동굴 트리거 기반으로 진입/이탈을 감지해 조명, 카메라, 캐릭터 상태를 원복하여 부드러운 전환 제공  
    
</details>

<br><br>

## Known Issues & Solutions


<br><br>

## Art Resources

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

## Sound Resources

- [Pixabay](https://pixabay.com)
  - [IntroBGM](https://pixabay.com/music/solo-instruments-magic-forest-318165/) → 인트로 배경 음악
    
  - [AdventureBGM](https://pixabay.com/music/upbeat-black-box-hypocrisy-112160/) → 전투 배경 음악
    
  - [Portal](https://pixabay.com/sound-effects/magic-teleport-whoosh-352764/) → 포탈 이동 효과음
    
  - [Gameover](https://pixabay.com/sound-effects/8bit-lose-life-sound-wav-97245/) → 캐릭터 사망 효과음
  
  - [MonsterDie](https://pixabay.com/sound-effects/super-deep-growl-86749/) → 몬스터 사망 효과음
    
  - [ItemPickup](https://pixabay.com/sound-effects/item-pickup-37089/) → 아이템 획득 효과음
  
- Asset Store
  - [TownBGM](https://assetstore.unity.com/packages/audio/music/electronic/8-bit-rpg-adventure-music-pack-184726), Track: 04 Overworld → 마을 배경 음악  

<br><br>

---

<div align="center">
  
⭐ **Thanks for taking a look at Verdant Valor!** ⭐

📌 **본 프로젝트는 개인 포트폴리오 용도로 제작되었습니다**  
저작권 및 기타 문제되는 부분이 있을 경우  
`every5116@naver.com` 으로 연락 주시면 신속히 대응하겠습니다

</div>
