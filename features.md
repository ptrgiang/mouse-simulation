# Mouse Workflow Automation — Features & Specification

## Overview
This document describes the two main features of your app and how they work together:

1. **Create Flow from Scratch** — a visual, node-based editor (Airflow-style) for designing mouse/keyboard automation.  
2. **Mouse Macro Recorder** — record live mouse activity and automatically convert it into an editable node-based workflow.

Both outputs use the same **node library** and **execution engine**, so workflows can be built manually or generated from recordings, then edited, scheduled, exported, and replayed.

---

## 1. Feature: Create Flow from Scratch (Node-based Editor)

### Purpose
Let users design reliable automation flows by dragging action nodes, connecting them into directed graphs, and editing node parameters.

### UI Layout (Core Panels)
- **Toolbox / Node Library**  
  List of node types:  
  `Move`, `Click`, `Drag`, `Image Click`, `Delay`, `Conditional`, `Loop`, `Keyboard`, `Script`, etc.  
  Drag from here to canvas.

- **Canvas / Graph View**  
  Place nodes, draw directed edges (arrows) to define execution order.  
  Supports:
  - Pan/zoom
  - Snap-to-grid
  - Auto-layout

- **Properties Panel**  
  Edit selected node’s parameters, add labels, attach templates (image file), set timing.

- **Inspector / Variables**  
  View workflow-level variables and logs.

- **Playback Controls**  
  Run, Step, Pause, Stop, Run from Node, Run Subflow.

- **Versioning / Save**  
  Save workflow JSON, export diagram image, import/export node groups (subflows).

- **Scheduler**  
  Schedule workflow runs (one-shot / cron / recurring / Windows Task Scheduler integration).

### Typical Node Types (Core)
- **Movement**: `MoveTo`, `MoveRelative`, `MoveAlongPath`
- **Click**: `SingleClick`, `DoubleClick`, `TripleClick`
- **Button States**: `MouseDown`, `MouseUp`, `ClickAndHold`, `Release`
- **Drag**: `DragFromTo`, `DragAlongPath`
- **Scroll/Wheel**: `ScrollVertical`, `ScrollHorizontal`
- **Image-driven**: `ImageClick`, `ImageWait`, `ImageExists`
- **Pixel/Text-driven**: `PixelCheck`, `OCRClick`
- **Control**: `Delay`, `ConditionalBranch`, `Loop`, `Goto`, `Try/Catch/ErrorHandler`
- **Integration**: `KeyboardInput`, `RunScript`, `HTTPRequest`, `SaveScreenshot`

### Workflow Creation Flow
1. Drag nodes to canvas and connect in desired order.
2. Configure node parameters in **Properties**.
3. Validate (check for unconnected nodes, cycles if disallowed).
4. Save as `.workflow.json`.
5. Run or schedule.

---

## 2. Feature: Mouse Macro Recorder → Auto Flow Generator

### Purpose
Record real user interactions and convert raw events into a human-readable, editable node-based workflow automatically.

### High-level Pipeline

#### Record
- Capture global low-level mouse hooks (movements, button down/up, wheel), keyboard events (optional), and timestamps.
- Optionally capture contextual screenshots or window title at key moments.

#### Pre-process
- Downsample movement events (~60Hz or less).
- Normalize timestamps relative to session start.

#### Segment & Heuristics
- Identify semantic actions:
  - LeftDown + moves + LeftUp → Drag (if movement distance > threshold)
  - LeftDown + LeftUp within short time & distance → Click
  - Two quick clicks → DoubleClick
  - Repeated small moves without clicks → Move or ignored (noise)
  - Long hold → ClickAndHold
- Detect context switches (window changes, large delays → new logical step)
- For GUI elements: capture small screenshot around click points to create templates for `ImageClick`.

#### Node Generation
- Convert segments to nodes with parameters:
  - coordinates
  - image template path
  - delay before action
  - inferred speed for drags

#### Post-process Grouping
- Collapse low-level sequences into higher-level steps (e.g., “Open menu → Select item” might become `ImageClick` + `Click`).
- Offer suggested names and grouping for readability.

#### Present on Canvas
- Open generated workflow in editor for user review and tuning.

---

### Example — Raw → Generated

**Raw events:**
```
00:00.100 Move (300,200)
00:00.300 LeftDown (300,200)
00:00.320 LeftUp (300,200)
00:00.900 Move (500,400)
00:01.000 LeftDown (500,400)
00:01.350 Move (800,600)
00:01.380 LeftUp (800,600)
```


**Auto-generated nodes:**
```json
[
  { "type": "MoveTo", "x":300, "y":200, "delayMs":0 },
  { "type": "Click", "button":"left", "x":300, "y":200, "delayMs":50 },
  { "type": "MoveTo", "x":500, "y":400, "delayMs":500 },
  { "type": "Drag", "x1":500, "y1":400, "x2":800, "y2":600, "delayMs":50, "speed":"natural" }
]
```


Editing and Feedback Loop
After generation, user can:

Replace coordinate-based nodes with ImageClick nodes (auto-crop screenshot saved during recording).

Insert Delay nodes or Conditional branches where appropriate.

Save resulting workflow; later recordings can be merged or appended.

Workflow File Format (JSON) — Example Schema

```json
{
  "id": "uuid",
  "name": "OpenPaint_DrawRectangle",
  "metadata": { "createdBy": "user", "createdAt": "2025-08-12T09:00:00Z" },
  "nodes": [
    { "id":"n1", "type":"LaunchApp", "params": { "exe":"mspaint.exe" }, "out":["n2"] },
    { "id":"n2", "type":"Delay", "params": { "ms":2000 }, "out":["n3"] },
    { "id":"n3", "type":"Click", "params":{ "x":100,"y":50,"button":"left" }, "out":["n4"] },
    { "id":"n4", "type":"Drag", "params":{ "x1":300,"y1":300,"x2":600,"y2":500,"speed":"medium" }, "out":["n5"] },
    { "id":"n5", "type":"SaveFile", "params":{ "filename":"rectangle.png" }, "out":["n6"] }
  ],
  "startNode": "n1"
}
```

out contains IDs of downstream nodes for graph traversal.

Conditional nodes include multiple out edges with labels:

```json
"out": [
  { "id":"nX","cond":"found" },
  { "id":"nY","cond":"notFound" }
]
```

Execution Engine (Notes)
Topology: Determine execution order by graph traversal from start nodes.
Supports:

Linear flows

Branching (if/else)

Loops (with safety counters)

Parallel branches (optional)

Context: Provide an execution context object carrying variables, last-match coordinates, screenshots.

Retry & Error Handling: Nodes can be set with retry count, timeout, and fallback nodes.

Safety: Pause on failed image match (option to wait or abort).

DPI & Multi-monitor: Store coordinates relative to target window when possible; otherwise include screen and monitor index. Support scaling conversion using system DPI.

Recording & Auto-generation Heuristics (Details)
Time thresholds: e.g., double-click if two clicks within 300ms and <6px distance.

Distance thresholds: drag vs click determined using e.g., >8–10 px movement.

Activity windows: group events separated by less than X seconds to same logical step.

Context capture: take a small screenshot around click/drag start/stop (e.g., 200×200 px) to generate image templates for ImageClick nodes.

Noise filtering: discard tiny cursor jitter outside of active interactions.

Window-aware mapping: if foreground window is constant, record coordinates relative to that window rectangle for better portability.

Robustness & Edge Cases
Window moved/resized: encourage image-based nodes or window-relative coordinates.

Scaling/DPI mismatch: convert coordinates to normalized values (percent of window) or use image matching.

Permissions: low-level hooks and SendInput typically require normal user privileges but may be blocked by some security software; explain UAC/elevation if needed.

Anti-cheat / AV: this kind of app resembles automation tools — be transparent and sign the app if used in sensitive environments.

Scheduling & Automation
Integrate with:

Quartz.NET for app-level scheduling (cron-like).

Windows Task Scheduler for system-level scheduling.

Support recurring schedules, time-window constraints, and run-on-login.

Image Recognition & OCR
Template matching: OpenCV (EmguCV for C#) — good for static UI elements.

Feature-based: SIFT/ORB for scaled/rotated icons (more robust).

OCR: Tesseract for text-based element detection (click-on-text).

Fallbacks: Pixel checks + small region match when template fails.

Security & Permissions
Explain to users why admin privileges may be requested:

Controlling other apps

Global hooks

Elevated UAC windows may require admin or accessibility permissions

Provide an explicit consent screen and a “safe mode” where only foreground app is controlled.

UX & Developer Tools
Undo / Redo stack

Find/Replace node parameters

Record Session Inspector to review raw events

Auto-layout nodes and collapse/expand subflows

Simulator mode (visual-only playback) to preview without moving the real mouse

Export to common formats (JSON, PNG diagram)

```
graph TD
  Start[Start] --> LaunchPaint[Launch: mspaint.exe]
  LaunchPaint --> WaitLoad[Delay: 2000ms]
  WaitLoad --> SelectRect[ImageClick: rect_tool.png]
  SelectRect --> MoveStart[MoveTo: (300,300)]
  MoveStart --> DragRect[Drag: (300,300)->(600,500)]
  DragRect --> SaveMenu[ImageClick: file_icon.png]
  SaveMenu --> SaveAs[Click: Save As]
  SaveAs --> TypeName[TypeText: rectangle.png]
  TypeName --> PressEnter[PressKey: Enter]
  PressEnter --> WaitSave[Delay:1000ms]
  WaitSave --> CloseApp[Shortcut: Alt+F4]
  CloseApp --> End[End]
```


