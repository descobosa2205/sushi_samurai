# Sushi Samurai 🍣⚔️  
Versión estilo **Fruit Ninja** desarrollada en **Unity (C#)** como proyecto final.  
Corta piezas de sushi para sumar puntos y evita las bombas: ¡una sola y se acaba la partida!

> Repo: https://github.com/descobosa2205/sushi_samurai.git

---

## 🎮 Descripción del juego
**Sushi Samurai** es un arcade rápido en el que controlas una katana (Blade) para cortar objetos arrojables lanzados en trayectorias.  
- Los **sushis** dan puntos.  
- Las **bombas** provocan **Game Over** si las cortas.  
- Un **oponente (IA)** puede aplicar acciones que aumentan la dificultad (por ejemplo aturdir la espada o acelerar el spawn).

---

## 🧠 Arquitectura y patrones de diseño
El proyecto está diseñado siguiendo un UML de clases y una arquitectura modular con responsabilidades claras.

### Patrones empleados
- **Factory (Factoría):** creación desacoplada de objetos arrojables (`IThrowable`) como `Sushi`, `Bomb`, etc.
- **Decorator (Decorador):** `SpecialSushi` añade funcionalidades extra a un `Sushi` base sin modificar la clase original.
- **Eventos:** comunicación desacoplada (p. ej. corte exitoso, bomba cortada, sushi perdido, acciones del oponente).

---

## ✅ Mecánicas principales
- Corte por deslizamiento / movimiento del input.
- Spawner con generación aleatoria.
- Sistema de puntuación centralizado en `GameManager`.
- Condición de derrota por bomba.
- IA enemiga basada en acciones (`IOpponentAction`).

---

## 🕹️ Controles
- **PC:**  
  - Movimiento del ratón para “cortar” con la katana (Blade).  
  - Key Space para pausar la partida.

---

## 🧩 Estructura (orientativa)

- `Assets/`
  - `Scripts/`
    - `GameManager.cs`
    - `Blade.cs`
    - `Spawner.cs`
    - `IThrowable.cs`
    - `Sushi.cs`
    - `Bomb.cs`
    - `SpecialSushi.cs`
    - `Opponent.cs`
    - `IOpponentAction.cs`
    - `BladeStun.cs`
    - `SpeedUpSpawnAction.cs`
  - `Prefabs/` (sushis, bombas, efectos)
  - `Scenes/`
  - `UI/`
- `Docs/` (UML, informe, documentación)

---

## 🚀 Instalación y ejecución
### Requisitos
- **Unity** 6000.2.7f2
> Nota: Estamos desarrollando el trabajo desde ordenadores Mac. Esto puede crear dependencias o dificultades a la hora de la ejecución.

### Pasos
1. Clona el repositorio:
   ```bash
   git clone https://github.com/descobosa2205/sushi_samurai.git