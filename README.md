# Verdant Valor Client Project (2025.07.01 ~ 2025.08.29)

![image](https://github.com/user-attachments/assets/9bacff69-a94f-4ae6-a570-01383085a5fc)


<br><br>


## 목차

1. [게임소개](#게임소개)
2. [기술스택](#기술스택)
3. [씬 플로우](#씬-플로우)
4. [유저 밸런스](#유저-밸런스)
5. [몬스터 밸런스](#몬스터-밸런스)
6. [아트 리소스](#아트-리소스)
7. [사운드 리소스](#사운드-리소스)


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

- [**시연 영상 보기**](https://www.youtube.com/watch?v=jal_0tfmpjY)


<br><br>


## 기술스택

| 항목 | 내용 |
|------|------|
| Engine | Unity 6000.0.46f1 |
| Language | C# |
| IDE | JetBrains Rider |

  
<br><br>


## 씬 플로우
<img width="700" height="500" alt="image" src="https://github.com/user-attachments/assets/4673a9d5-ad31-4952-bdf3-205f54eb477a" />


<br><br>


## 유저 밸런스

| Level | HP  | ATK | 누적 경험치 (EXP) |
|-------|-----|-----|------------|
| 1     | 100 | 3.0 | 100        |
| 2     | 150 | 3.5 | 300        |
| 3     | 220 | 4.2 | 700        |
| 4     | 300 | 5.0 | 1200       |
| 5     | 400 | 6.0 | 2000       |


<br><br>


## 몬스터 밸런스

| 몬스터       | HP   | Speed | Attack Time | Damage | Trace Distance | Attack Distance | 특징                  | 획득 경험치 (EXP) |
|--------------|------|-------|-------------|--------|----------------|-----------------|-----------------------|----|
| FlyingEye    | 10f  | 3f    | 1.5f        | 1f     | 7f             | 1.5f            | 빠르고 자주 공격하지만 약함 | 5  |
| Goblin       | 20f  | 2f    | 2f          | 3f     | 5f             | 1.2f            | 밸런스형, 다수일 때 위험   | 12 |
| Mushroom     | 35f  | 0.8f  | 3f          | 2f     | 4f             | 1f              | 느리고 단단한 탱커   | 20 |
| Skeleton     | 15f  | 1.5f  | 1.8f        | 4f     | 6f             | 1.3f            | 공격력 높지만 체력 약함   | 15 |


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
  
- [Asset Store](https://assetstore.unity.com)
  - [TownBGM](https://assetstore.unity.com/packages/audio/music/electronic/8-bit-rpg-adventure-music-pack-184726), Track: 04 Overworld → 마을 배경 음악  

<br><br>

---

<div align="center">

   ⭐ **Thanks for taking a look at Meow Jump Game!** ⭐

   📌 **본 프로젝트는 개인 포트폴리오 용도로 제작되었습니다**  
   저작권 및 기타 문제되는 부분이 있을 경우  
   `every5116@naver.com` 으로 연락 주시면 신속히 대응하겠습니다
   
</div>
