# Blender 에셋 내보내기 점검 목록

몬스터와 보스를 만들 때마다 위에서 아래로 순서대로 확인하세요. 핵심 원칙은 두 가지입니다. **모양을 굳히는 작업(모디파이어 Apply, 크기 Apply)이 먼저**이고, **움직임과 관련된 작업(피봇, 부모 구조, 리깅)은 모양이 확정된 뒤**에 합니다.

## 1. 시작 전 기준

- [ ] 단위는 Blender 1m = Unity 1유닛으로 두고, 캐릭터 크기는 Dimensions(N 패널)에서 확인했다
- [ ] 캐릭터 정면이 Front 뷰(키패드 1)에서 보이는 방향(-Y)이다
- [ ] 파츠 이름을 `캐릭터_파츠` 형태로 지었다 (공백과 한글 제외, 메시 데이터와 머티리얼 이름도 함께 정리)
- [ ] 폴리곤 예산(Statistics의 Triangles 합계)을 정했다

## 2. 파츠 모양 만들기

- [ ] 휘게 하거나 둥글릴 파츠는 둘레 Vertices 12 이상, 길이 방향 선(Ctrl+R)은 10개 이상이다
- [ ] 크기와 비율은 Apply 전에 먼저 정했다 (Scale이 1이 아닌 채 Bevel이나 Bend를 걸면 모양이 찌그러진다)
- [ ] 끝을 둥글리는 Bevel은 Bend보다 먼저 적용한다
- [ ] 모양이 확정되면 Shade Smooth를 한다 (면을 추가하거나 Join한 뒤에도 다시 확인)
- [ ] 작은 파츠의 폴리곤이 과하지 않다 (눈 500 이하, 입과 혓바닥 각 500 이하 권장)

## 3. 크기 확정과 Apply

순서가 중요합니다. 모디파이어를 먼저 확정하고, 그다음 Transform을 Apply합니다.

- [ ] Modifiers 탭의 모디파이어(Bevel, Simple Deform, Solidify 등)를 모두 ▾ → Apply 했다
- [ ] S 키로 크기를 맞추고 원하는 위치로 옮겼다
- [ ] Ctrl+A → All Transforms로 Apply했다
- [ ] N 패널에서 Scale `1, 1, 1`, Rotation `0, 0, 0`을 확인했다 (Scale 초기값은 0이 아니라 1)
- [ ] 여러 파츠를 Join하기 전에 각 파츠를 먼저 Apply했다

## 4. 피봇 정하기

피봇은 모양과 배치가 끝난 뒤, 내보내기 직전에 잡습니다. 방법은 Edit Mode에서 기준 점이나 면을 선택하고 Shift+S → Cursor to Selected를 한 뒤, Object → Set Origin → Origin to 3D Cursor를 누르는 것입니다.

| 파츠 | 피봇 위치 | 이유 |
| --- | --- | --- |
| 루트(Empty) | 발밑, 월드 원점 | Unity에서 바닥(Y=0)에 맞게 서고 바닥을 뚫지 않는다 |
| 눈 | 구의 중심 | 제자리에서 회전하고 깜빡인다 |
| 입 | 파츠 중심 | 움직임이 거의 없다 |
| 혓바닥 | 입에 붙는 뿌리 | 뿌리는 고정되고 끝이 움직인다 |

- [ ] 파츠별 피봇을 위 표대로 잡았다 (Apply를 하면 원점이 월드 원점으로 돌아가므로, 피봇은 Apply 뒤에 정한다)
- [ ] 도구를 드는 파츠의 끝에는 Empty 소켓을 만들었다

## 5. 오브젝트 계층 구조

- [ ] 빈 오브젝트 루트(`Player`)를 만들고 몸체, 눈, 입, 혓바닥을 자식으로 넣었다 (자식을 먼저, 부모를 마지막에 선택 → Ctrl+P → Object (Keep Transform))
- [ ] 루트의 Location `0,0,0`, Rotation `0,0,0`, Scale `1,1,1`이다 (자식의 Location은 0이 아니어도 정상)
- [ ] 스크린샷이나 Outliner에서 계층이 의도대로인지 확인했다

## 6. 머티리얼

- [ ] 파츠별로 머티리얼을 나누고 `M_캐릭터_파츠`로 이름 지었다
- [ ] 작은 파츠는 단색 Base Color로 충분하다 (복잡한 셰이더 노드는 Unity로 넘어가지 않는다)
- [ ] 같은 색은 머티리얼 하나를 재사용했다

## 7. 리깅 (움직이는 에셋만)

리깅 전에는 반드시 저장하고 커밋합니다. 리깅 뒤에 몸체를 Apply하거나 Scale을 바꾸면 뼈와 어긋납니다.

- [ ] 뼈대는 뿌리(바닥 쪽)부터 머리 쪽으로 한 줄로 이어 만들었다 (Pose Mode에서 뼈를 돌렸을 때 머리 쪽만 따라오면 방향이 맞다)
- [ ] 연습용으로 만든 불필요한 뼈나 길이가 거의 0인 뼈가 없다 (Outliner에서 계층을 펼쳐 확인)
- [ ] 뼈 이름을 `Spine_01`, `Head`, `Tongue_01`처럼 순서가 보이게 정리했다 (오타 확인)
- [ ] Edit Mode에서 Shift+N으로 Roll을 정리했다
- [ ] 혓바닥처럼 따로 움직일 뼈는 머리 쪽 뼈의 자식으로 연결했다 (Ctrl+P → Keep Offset)
- [ ] 몸체는 Ctrl+P → With Automatic Weights로 연결했고, Pose Mode에서 자연스럽게 휘는지 확인했다
- [ ] 눈, 입, 혓바닥, 소켓은 Relations에서 Parent Type이 `Bone`이고 Parent Bone이 맞다 (Outliner에는 뼈 아래가 아니라 Armature 아래에 보이는 것이 정상)
- [ ] Pose Mode에서 확인한 뒤 Alt+R(Alt+G)로 자세를 초기화했다

## 8. FBX 내보내기

- [ ] 루트와 모든 자식(메시, Armature, Empty)을 선택하고 Selected Objects를 체크했다
- [ ] Object Types에 Armature, Empty, Mesh가 포함되어 있다
- [ ] Apply Transform을 체크했고, 숨겨진 오브젝트(눈 아이콘이 꺼진 것)가 없다
- [ ] 리깅한 에셋은 Add Leaf Bones를 껐다
- [ ] 텍스처를 쓰면 같이 내보냈거나 Unity 프로젝트에 파일을 넣었다

## 9. Unity에서 확인

- [ ] 씬에 놓았을 때 Rotation `0,0,0`, Scale `1,1,1`이고 바닥에 맞게 선다
- [ ] 얼굴이 파란 화살표(Z축, forward) 방향을 본다 (Scene 뷰는 Pivot / Local 모드로 확인)
- [ ] 파츠별 피봇 위치와 소켓 위치가 의도대로이다
- [ ] Rig → Animation Type을 Generic으로 설정했다
- [ ] 머티리얼이 분홍색이면 셰이더를 URP/Lit으로 바꿨다
- [ ] 실제 게임 카메라 거리에서 거슬리는 부분만 고쳤다 (미세한 외형에 오래 머물지 않기)

## 10. 저장과 기록

- [ ] `.blend`는 `BlenderSource/` 폴더에 두었고, `.blend1`과 `.blend@`는 `.gitignore`로 제외했다
- [ ] Git LFS로 `.blend`와 `.fbx`가 추적되는지 `git lfs ls-files`로 확인했다
- [ ] 커밋 메시지에 변경 내용을 구체적으로 썼다 (예: 몸체 초안은 Tripo, 나머지는 직접 제작)
- [ ] 이번 에셋의 제작 시간을 기록했다 (다음 몬스터와 비교해 일정 계산에 쓴다)
