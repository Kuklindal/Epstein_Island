\# Epstein Island Horror



\## Описание проекта



3D хоррор-игра от первого лица. Игрок исследует остров, собирает предметы и пытается сбежать.



---



\# Основные механики



\## Player Controller



Файл: `Scripts/Player/PlayerController.cs`



Функции:



\* движение WASD

\* коллизии через CharacterController



Кнопки:



\* W A S D — движение



---



\## Camera System



Файл: `Scripts/Player/MouseLook.cs`



Функции:



\* вращение камеры

\* ограничение вертикального угла



Кнопки:



\* Mouse — управление камерой



---



\## Interaction System



Файл: `Scripts/Interactions/InteractionSystem.cs`



Функции:



\* Raycast из камеры

\* взаимодействие с объектами

\* проверка слоя Interactable



Кнопки:



\* E — взаимодействие



---



\## Flashlight System



Файл: `Scripts/Systems/FlashlightSystem.cs`



Функции:



\* включение / выключение

\* управление интенсивностью



Кнопки:



\* F — включить фонарик

\* Scroll — изменить яркость



---



\## Inventory System



Файл: `Scripts/Inventory/InventorySystem.cs`



Функции:



\* хранение списка предметов

\* добавление предмета

\* проверка наличия



Тип предметов:

ScriptableObject (`ItemData`)


UI Prompt System

Файл: Scripts/Interactions/InteractionSystem.cs

Функции:

отображение подсказки взаимодействия

проверка Raycast на объект

проверка наличия компонента взаимодействия

UI элемент:

Canvas → InteractionPrompt

Поведение:

появляется при наведении на предмет

исчезает при отведении камеры

работает только для объектов со слоем Interactable

Текст подсказки:

Press E
Enemy AI System

Файл: Scripts/Enemy/EnemyFSM.cs

Система искусственного интеллекта врага реализована через Finite State Machine (FSM).

Состояния врага:

Idle — ожидание

Patrol — патрулирование

Chase — преследование игрока

Attack — атака

Patrol State

Функции:

движение между точками патруля

использование системы навигации Unity (NavMeshAgent)

зацикливание маршрута

Параметры:

PatrolPoints (массив точек)

Поведение:

точка 1 → точка 2 → точка 3 → точка 1
Навигация

Используется компонент:

NavMeshAgent

Позволяет врагу:

обходить препятствия

автоматически строить маршрут

двигаться по NavMesh поверхности

Отладка

Состояние врага выводится в консоль:

Debug.Log("Enemy: Patrol")

Это позволяет отслеживать текущее состояние FSM во время разработки.
