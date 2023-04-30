# ConsoleSpinner
This library is a simple console spinner for .NET Core Console applications. It can be used for synchrouns blocks of code or async tasks.  

# Styles
The spinner can be displayed in the following styles:
| Style | Description | 
| ----- | ----------- | 
| Lines | Displays a line spinner | 
| Dots | Displays a dot spinner | 
| Boxes| Displays a box spinner | 
| Arrows | Displays an arrow spinner | 
| VerticalBars | Displays a vertical bar spinner | 
| HorizontalBars | Displays a horizontal bar spinner | 
| Triangles | Displays a triangle spinner | 
| QuarterBalls | Displays a quarter ball spinner | 
| HalfBalls | Displays a half ball spinner | 
| Balloons | Displays a balloon spinner | 
| Arcs | Displays an arc spinner | 
| BouncingBalls | Displays a bouncing ball spinner | 
| Wave | Displays a wave spinner |
| Braille | Displays a braille spinner | 
 

# Usage 
The library adds a new helper function ```ConsoleEx.WriteSpinner()```.  This function returns an IDisposable that will display the spinner until it is disposed, 
or you can pass in a task and it will display the spinner until the task is complete.

This allows the spinner to be used in two ways, with long running synchronous code or with async tasks.

# Usage with synchronous code
With synchronous code you can simply wrap the code in a using statement and the spinner will be displayed until the code block is complete.

```csharp	
using(_ = Console.WriteSpinner())
{
	// long running non-task based code 
}
```

## Usage with async Tasks
With async tasks you simply pass the task into ```ConsoleEx.WriteSpinner()``` and it will animate the spinner until the task is complete.

```csharp
var task = Task.Delay(delay);
lock (Console.Out)
{
    ConsoleEx.WriteSpinner(SpinnerStyle.Lines, task);
    Console.WriteLine();
}
```

# Adding status text with spinner.
You can customize the spinner by passing in a *CustomFrame* function. The custom frame function takes in the current frame, and a boolean for whether the task is done or not. 
It returns a string that represents the frame. 

Arguments are
* frame - the current frame of the animation
* done - a boolean indicating whether the task is done or not
Return:
* A formatted string representing the frame. 

> NOTE: The width of the output and position of the frame in the output needs to be consistent for the animation to look correct.

```csharp
i = 10;
using (var _ = ConsoleEx.WriteSpinner(SpinnerStyle.Lines, customFrame: (frame, done) => $"{frame} Counter: {i} "))
{
    for (; i > 0; i--)
    {
        // simulate doing stuff...
        await Task.Delay(1000);
    }
}
```

This allows you to have the spinner act like a progress bar.

# Animation Notes
The animation is asynchronously using Console.SetPosition to position the cursor to the location of the animation so that it can change it.  
This means that the animation will not work correctly if you are writing to the console at the same time.  
To prevent that you should use a ```lock(Console.Out)``` around any Console output API call, preventing any background animations from corrupting the cursor position.

Example:
```csharp
using (var _ = ConsoleEx.WriteSpinner())
{
    ...long running code..
    lock(Console.Out)
    {
        Console.Write(...);
    }
    ...long running code..
}
```
