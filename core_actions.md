# Core Action Nodes Specification
Node-based workflow actions for simulating all mouse functions and movements.

---

## 1. Movement Nodes

### **Move to Coordinates**
- **Description:** Move cursor to a specific screen position.
- **Parameters:**
  - `X`: Horizontal position (pixels)
  - `Y`: Vertical position (pixels)
  - `speed`: Movement speed (optional, default: instant)
  - `easing`: Movement easing style (linear, ease-in, ease-out)

### **Move Relative**
- **Description:** Move cursor relative to current position.
- **Parameters:**
  - `ΔX`: Horizontal offset (pixels)
  - `ΔY`: Vertical offset (pixels)
  - `speed`: Movement speed

### **Move Along Path**
- **Description:** Move cursor through a set of coordinates, optionally simulating human-like jitter.
- **Parameters:**
  - `path`: Array of `(X, Y)` coordinates
  - `speed`: Average movement speed
  - `jitter`: Random variation in path

---

## 2. Click & Press Nodes

### **Single Click**
- **Description:** Perform a single mouse click.
- **Parameters:**
  - `button`: left | right | middle
  - `X` (optional): Target X coordinate
  - `Y` (optional): Target Y coordinate

### **Double Click**
- **Description:** Perform a double mouse click.
- **Parameters:**
  - `button`: left | right | middle
  - `interval`: Time between clicks (ms)

### **Triple Click**
- **Description:** Perform three quick clicks.
- **Parameters:** Same as Double Click

### **Mouse Down**
- **Description:** Press and hold a mouse button.
- **Parameters:**
  - `button`: left | right | middle
  - `X` (optional), `Y` (optional)

### **Mouse Up**
- **Description:** Release a pressed mouse button.
- **Parameters:** Same as Mouse Down

---

## 3. Drag & Drop Nodes

### **Drag From–To**
- **Description:** Click-hold at `(X1, Y1)`, move to `(X2, Y2)`, release.
- **Parameters:**
  - `X1`, `Y1`: Start position
  - `X2`, `Y2`: End position
  - `speed`: Movement speed

### **Drag Relative**
- **Description:** Drag from current position by offset.
- **Parameters:**
  - `ΔX`, `ΔY`: Relative movement
  - `speed`: Movement speed

### **Drag Along Path**
- **Description:** Drag through multiple waypoints before releasing.
- **Parameters:**
  - `path`: Array of `(X, Y)` coordinates
  - `speed`: Movement speed

---

## 4. Scroll & Wheel Nodes

### **Scroll Up / Down**
- **Description:** Scroll vertically.
- **Parameters:**
  - `amount`: Number of scroll units
  - `speed`: Scroll speed

### **Scroll Left / Right**
- **Description:** Scroll horizontally.
- **Parameters:** Same as vertical scroll

### **Scroll to Element/Image**
- **Description:** Scroll until a target image or element is visible.
- **Parameters:**
  - `target`: Image or element reference
  - `direction`: up | down | left | right

---

## 5. Advanced Interaction Nodes

### **Click on Image**
- **Description:** Find and click on a target image on screen.
- **Parameters:**
  - `image`: Path to image file
  - `match_threshold`: Similarity percentage (0-100)

### **Click on Color**
- **Description:** Find and click a pixel with the target color.
- **Parameters:**
  - `color`: RGB or HEX value
  - `tolerance`: Allowed color difference

### **Click on Text (OCR)**
- **Description:** Find text using OCR and click its position.
- **Parameters:**
  - `text`: String to match
  - `match_mode`: exact | partial

### **Hover Over Target**
- **Description:** Move cursor to target without clicking.
- **Parameters:** Same as Click on Image / Color / Text

### **Right-Click Context Menu Selection**
- **Description:** Right click, then select a menu option.
- **Parameters:**
  - `option_text`: Menu item label
  - `match_mode`: exact | partial

### **Click and Hold**
- **Description:** Long-press before releasing.
- **Parameters:**
  - `duration`: Hold time (ms)
  - `button`: left | right | middle

---
