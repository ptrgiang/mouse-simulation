# Mouse Workflow Automation App - C#

## Overview
The **Mouse Workflow Automation App** is a Windows-based desktop application designed to **record, simulate, and schedule** mouse actions in a **visual workflow editor**.  
It allows users to create step-by-step automation sequences for clicks, drags, moves, and image-based actions, similar to how Apache Airflow visualizes workflows.

---

## Key Features

### 1. **Visual Workflow Editor**
- Drag-and-drop interface for creating automation flows.
- Nodes represent mouse actions such as:
  - **Click** (Left/Right at specific X/Y coordinates)
  - **Move** (Move cursor to specified X/Y)
  - **Drag & Drop** (From X1/Y1 to X2/Y2)
  - **Image Click** (Locate and click on a provided image on screen)
  - **Right Click** (Context menu actions)
  - **Delay** (Pause for a specified time)
- Connect nodes with arrows to define execution order.

### 2. **Recording Mode**
- Record actual mouse movements and clicks.
- Save the recorded sequence into the workflow editor.
- Option to fine-tune coordinates and parameters after recording.

### 3. **Playback Engine**
- Executes actions in the order defined in the workflow.
- Uses Windows APIs (`SetCursorPos`, `SendInput`) for precise mouse control.
- Supports image recognition (via OpenCV) for dynamic clicking.

### 4. **Scheduling**
- Run workflows immediately or schedule for later.
- Supports repeat intervals (hourly, daily, weekly).

### 5. **Workflow Management**
- Save workflows to JSON or XML files.
- Load, edit, and reuse saved workflows.
- Export workflow diagrams as images for documentation.

---

## Technical Stack
- **Language:** C# (.NET WPF)
- **UI Framework:** WPF with drag-and-drop canvas
- **Mouse Control:** Windows API (`SetCursorPos`, `SendInput`)
- **Image Matching:** OpenCV via Emgu CV for C#
- **Data Storage:** JSON for workflow save/load
- **Scheduling:** Quartz.NET for task scheduling

---

## Target Use Cases
- Automating repetitive desktop tasks.
- Testing and simulating UI interactions.
- Creating repeatable workflows for data entry.
- Simplifying image-based clicks without hardcoding coordinates.

---

## Advantages
- User-friendly visual workflow interface.
- Step-by-step action tracking.
- Precise and repeatable mouse automation.
- Fully customizable and schedulable workflows.
