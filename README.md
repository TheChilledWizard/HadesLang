<img src="https://raw.githubusercontent.com/Azer0s/HadesLang/master/HadesLang/IconLong.png" /> 

***

[![Build Status](https://travis-ci.org/Azer0s/HadesLang.svg?branch=master)](https://travis-ci.org/Azer0s/HadesLang)
[![License](https://img.shields.io/badge/license-MIT-brightgreen.svg)](https://github.com/Azer0s/HadesLang/blob/master/LICENSE)


*Hades is a embedded scripting language built from scratch!*
<br>
Here are a few examples of the language in action:

## Register a custom function in C#
```cs
var interpreter = new Interpreter(new ConsoleOutput());
interpreter.RegisterFunction(new Function("print", a =>
{
    a.ToList().ForEach(Console.WriteLine);
}));
```

## Hello world
```vb
out:'Hello world'
```

## Fibonacci Numbers
### fibonacci.hades
```vb
func fib[]
    a as num closed = 1
    b as num closed = 1

    asLongAs[true]
        out:a
        out:b

        a = $a + $b
        b = $a + $b

        case[$a bigger 10000]
	    put a
        endCase
    endAsLongAs
endfunc
```
### main.hades
```vb
load:'fibonacci.hades' as a
b as num reachable = $a->fib:[]
out:' '
out:b
```
## Force value through interpreter 

```vb
a as word closed = 'out:b'
b as word closed = 'Hello world'
#a
```
Output will be Hello world

## Overarching variables
### foo.hades
```vb
a as num reachable_all = 18
load:'bar.hades' as b
$b->printA:[]
```
### bar.hades
```vb
func printA[]
    out:a
endfunc
```

## Tasking
```vb
task as t
    a as num closed = 0
    asLongAs [true]
        out:'Hello world'+$a
    endAsLongAs
endTask

startTask:t
```

## Conditions
```vb
case[sqrt(9) smallerIs 3]
    out:'The squareroot of 9 is smaller/equals 3'
endcase
```

## Unloading
```vb
a as num reachable = 18
uload:a
```
