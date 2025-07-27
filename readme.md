# ConsoleSpinner
This library is a simple console spinner for .NET Core Console applications. It can be used for synchronous blocks of code or async tasks.  

![demo.gif](https://raw.githubusercontent.com/tomlm/ConsoleSpinner/main/demo.gif)

# Usage 
The library adds a new helper function ```ConsoleEx.StartSpinner()```.  This function returns an IDisposable that will display the spinner until it is disposed, 
or you can pass in a task and it will display the spinner until the task is complete.

This allows the spinner to be used in two ways, with long running synchronous code or with async tasks.

## Setup
This library uses background tasks to write to the console output. In order for this to work
without corrupting the console output, you need to add a reference to the ```Nito.AsyncEx``` nnuget package and set up a single threaded synchronization context for your console application.

```csharp
class Program
{
  static int Main(string[] args)
    // set up synchornization context for console APIs
     => AsyncContext.Run(AsyncMain);

  static async Task<int> AsyncMain()
  {
    ... your console code here ...
  }
}
```

## Usage with synchronous code
The spinner is an IDisposable which is useful for synchronous code. Just use a using statement to create a scope for the spinner. The spinner will animate until the block is exited and the object is disposed.

```csharp	
// spinner will complete when block is exited.
using(_ = ConsoleEx.StartSpinner())
{
	// long running non-task based code 
}
```

## Usage with async Tasks
The spinner lifetime can be tied to the lifetime of a Task. This is useful for async code where you want to display a spinner while waiting for a task to complete. The spinner will animate until the task is completed. 

```csharp
// spinner will complete when task is completed.
var task = Task.Delay(delay);
ConsoleEx.StartSpinner(task);
await task; // wait for the task to complete
```

# Using Options
You can control the behavior of the spinner by passing in a SpinnerOptions object to ```ConsoleEx.StartSpinner()```.    

## Options.Animation
You can pass in a different animation style by passing in the Options to ```ConsoleEx.StartSpinner()```.
```csharp	
using(_ = ConsoleEx.StartSpinner(Animations.Dots))
{
	// long running non-task based code 
}
```

Predefined animations are:
The spinner can be displayed in the following styles:
| Style | Description | 
| ----- | ----------- | 
| Animations.Lines | Displays a line spinner | 
| Animations.Dots | Displays a dot spinner | 
| Animations.Boxes| Displays a box spinner | 
| Animations.Arrows | Displays an arrow spinner | 
| Animations.VerticalBars | Displays a vertical bar spinner | 
| Animations.HorizontalBars | Displays a horizontal bar spinner | 
| Animations.Triangles | Displays a triangle spinner | 
| Animations.QuarterBalls | Displays a quarter ball spinner | 
| Animations.HalfBalls | Displays a half ball spinner | 
| Animations.Balloons | Displays a balloon spinner | 
| Animations.Arcs | Displays an arc spinner | 
| Animations.BouncingBalls | Displays a bouncing ball spinner | 
| Animations.Wave | Displays a wave spinner |
| Animations.Braille | Displays a braille spinner | 

You can also defined your own animations by passing in a array of equal width strings. The animation will cycle through the strings.  

```csharp
using(var _ = ConsoleEx.StartSpinner(new SpinnerOptions() { Animation= new [] { "`  ", "`` ", "```", " ``", "  `", "   "}))
{
	...long running code..
}
```


## Options.Theme
The options.Theme is an array of ConsoleColors which will be used for each frame.  The spinner will cycle through the colors.  
```csharp
using(_ = ConsoleEx.StartSpinner(new SpinnerOptions() { Theme = Themes.RedWhiteBlue }))
{
	// long running non-task based code 
}
```

## Options.Delay
Delay controls how many ms between each frame of the animation.  The default is 100ms.

## Options.Success, Options.Failure, Options.SuccessColor, Options.FailureColor
These options all you to define the string for success, failure, and the color for each.  


## Options.CustomFrame
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
using (var _ = ConsoleEx.StartSpinner(new SpinnerOptions() { CustomFrame: (frame, done) => $"{frame} Counter: {i} " }))
{
    for (; i > 0; i--)
    {
        // simulate doing stuff...
        await Task.Delay(1000);
    }
}
```

This allows you to have the spinner act like a progress bar.

# Change Log
## 2.x to 3.x
* Switched to using Nito.AsyncEx for synchronization context. This is required for console applications to use async/await without corrupting the console output. (see https://devblogs.microsoft.com/dotnet/await-synchronizationcontext-and-console-apps/ for details)
* Changed WriteSpinner() => StartSpinner() to better reflect the usage of the spinner.
* Added additional method overloads for animation and success/failed to make it easier to invoke 
* updated readme