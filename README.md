# Simple-VHDL-verificator

A small Windows desktop tool from 2021 that checks the output file of a VHDL design against a
reference result computed in C#. The window title and labels are in Russian
(«Верификатор»).

## What it checks (from `Verificator/MainWindow.xaml.cs`)

- **Input file** — a square adjacency matrix of a graph written as rows of `0`/`1`, one row per
  line (Windows `CRLF` line endings are expected).
- The tool finds the **largest connected component** of the graph and writes the reference
  result to `out.txt` in the working directory: first line — one `0`/`1` flag per vertex
  (whether it belongs to the component), second line — the component size.
- **Output file** — the file produced by the VHDL simulation. The tool shows its contents and
  compares it with `out.txt` character by character, then reports
  «Результаты совпадают!» (results match) or «Результаты не совпадают!» (results differ).

## Usage

1. «Файл с входными значениями» → «Открыть файл»: pick the matrix file.
2. «Файл с выходными значениями» → «Открыть файл»: pick the VHDL output file.
3. Click «Проверить файл».

Example input for 4 vertices (vertices 0–1–2 connected, vertex 3 isolated):

```
0110
1010
1100
0000
```

## Stack

- C#, WPF, .NET Framework 4.7.2
- Visual Studio solution `Verificator.sln`

## Build and run

Open `Verificator.sln` in Visual Studio 2019 or newer with the *.NET desktop development*
workload, build and run. Not verified here: WPF on .NET Framework needs Windows. The expected
output for the example above was not produced by running the tool either.

## Context

A student project (2021) made to verify a VHDL implementation of the same graph task.

## License

Public domain — see [LICENSE](LICENSE) (The Unlicense).
