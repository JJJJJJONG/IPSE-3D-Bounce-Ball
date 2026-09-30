<h1 align="center">🟡 Bounce Ball 3D</h1>

<p align="center">
  추억의 게임 <b>'바운스볼'</b>을 3D로 재해석한 Unity 게임<br>
  IPSE 2023 Unity 세미나 프로젝트 (2023.11)
</p>

<p align="center">
  <img src="https://img.shields.io/badge/Unity-2021.3-000000?style=for-the-badge&logo=unity&logoColor=white">
  <img src="https://img.shields.io/badge/C%23-239120?style=for-the-badge&logo=csharp&logoColor=white">
  <img src="https://img.shields.io/badge/Blender-F5792A?style=for-the-badge&logo=blender&logoColor=white">
</p>

<p align="center">
  <img src="docs/images/main_menu.jpg" width="80%">
</p>

## 📌 프로젝트 소개

바닥에 닿을 때마다 자동으로 튀어 오르는 공을 조작해 스테이지의 **별을 모두 모으면 클리어**하는 3D 퍼즐 액션 게임입니다.
2D 고전 게임 바운스볼의 규칙을 3D 공간으로 옮겨, 카메라를 돌려 가며 입체적으로 길을 찾도록 만들었습니다.
블록 3D 모델은 Blender로 제작했습니다.

## 🎮 주요 기능

| 분류 | 내용 |
| --- | --- |
| **자동 바운스** | 바닥·블록에 닿으면 항상 같은 높이로 튀어 오르는 물리 기반 이동 |
| **특수 블록** | 🔼 점프 블록(2배 점프) · 한 번 밟으면 사라지는 블록 · ⏩ 비행 블록(일직선으로 날아감) |
| **스테이지 진행** | 별을 모두 모으면 다음 스테이지, 떨어지면 해당 스테이지 재시작 |
| **스테이지 해금** | 15칸 스테이지 선택 화면, 클리어 기록을 저장(PlayerPrefs)해서 다음 스테이지 잠금 해제 |
| **3인칭 카메라** | 마우스로 공 주위를 자유롭게 회전, 카메라 방향 기준 WASD 이동 |
| **일시정지** | Q 일시정지 / R 계속하기, 홈·재시도 버튼 |
| **사운드** | 싱글톤 AudioManager로 씬이 바뀌어도 효과음 유지 |

## 🖼 스크린샷

| 스테이지 선택 | 플레이 화면 |
| :---: | :---: |
| <img src="docs/images/stage_select.jpg"> | <img src="docs/images/gameplay.jpg"> |

<p align="center"><img src="docs/images/level_overview.jpg" width="60%"><br><sub>스테이지 전체 모습</sub></p>

## 🕹 조작법

| 키 | 동작 |
| --- | --- |
| W A S D | 이동 (카메라 방향 기준) |
| 마우스 | 시점 회전 |
| 방향키 입력 (비행 중) | 비행 멈추기 |
| Q / R | 일시정지 / 계속하기 |

## 🛠 기술 스택 & 구조

- **Engine**: Unity 2021.3.14f1 (3D Physics, TextMeshPro, uGUI)
- **Language**: C#
- **3D Modeling**: Blender (점프·비행·한 번 밟는 블록, 잔디 블록)

```
IPSE 3D Bounce Ball/Assets/Scripts
├── Ball.cs            # 이동, 자동 바운스, 특수 블록 효과, 비행, 별 수집
├── GameManager.cs     # 스테이지 클리어 판정, 진행도 저장, 스테이지 이동/재시작
├── UIManager.cs       # 스테이지 선택 화면 잠금/해금 표시
├── PauseUIManager.cs  # 일시정지 / 홈 / 재시도
├── CameraMove.cs      # 마우스 3인칭 궤도 카메라
├── AudioManager.cs    # 효과음 싱글톤
└── ObjectBounce.cs    # 별 회전·둥실거림 연출
```

### 폴더 구성

| 폴더 | 내용 |
| --- | --- |
| `IPSE 3D Bounce Ball/` | **메인 Unity 프로젝트** |
| `3D Model/` | 블록 모델 원본 (.blend / .fbx) |
| `Image/`, `Sound/` | UI 이미지와 효과음 원본 |
| `My project/` | 초기 프로토타입 (공 이동·카메라) |
| `IPSE_Semina/` | 세미나 실습용 프로젝트 |

## ▶ 실행 방법

1. [Releases](../../releases)에서 Windows 빌드를 받아 `Bounce.exe` 실행
2. 또는 Unity Hub에서 **Unity 2021.3.14f1**로 `IPSE 3D Bounce Ball/` 폴더를 열고 `Assets/Scenes/MainMenuUI.unity` 실행
