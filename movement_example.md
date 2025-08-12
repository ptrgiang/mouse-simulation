Task: Open a graphics app, draw a rectangle, save the file, and close the app.

graph TD
    Start[Start Workflow] --> LaunchApp[Launch Application: Paint.exe]
    LaunchApp --> WaitApp[Delay 2000ms - Wait for App Load]

    WaitApp --> MoveToolbar[Move To Toolbar Icon (X=100,Y=50)]
    MoveToolbar --> LeftClickToolbar[Left Click - Select Rectangle Tool]
    
    LeftClickToolbar --> MoveStartDraw[Move To Canvas Start (X=300,Y=300)]
    MoveStartDraw --> DragToEnd[Drag to (X=600,Y=500)]
    DragToEnd --> Release[Release Mouse Button]

    Release --> MenuFile[Move To File Menu (Image Match: file_icon.png)]
    MenuFile --> LeftClickFile[Left Click]
    LeftClickFile --> SaveAs[Move To Save As (Y=150 from Menu)]
    SaveAs --> LeftClickSaveAs[Left Click]
    
    LeftClickSaveAs --> EnterFileName[Type Text: "rectangle.png"]
    EnterFileName --> PressEnter[Press Key: Enter]
    PressEnter --> WaitSave[Delay 1000ms]

    WaitSave --> CloseApp[Close App Shortcut (Alt+F4)]
    CloseApp --> End[End Workflow]
