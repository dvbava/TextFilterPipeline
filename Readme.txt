

Implementation Notes
	- Split the solution into a reusable Core library, Console application and Test project to keep responsibilities separate.
	- Program.cs acts only as the composition root. All processing is delegated to the orchestrator.
	- Filters implement a common ITextFilter interface, making it easy to add or remove filtering rules without changing the pipeline.
	- Where applicable, filters are generalized to accept similar rules (e.g., the ContainsCharFilter accepts any character, not just hard wired to 't'.).
	- The pipeline applies filters sequentially and remains independent of file or console I/O.
	- Reading uses File.ReadLines() with yield return so the application processes files lazily instead of loading the entire file into memory.
	- The design keeps each class focused on a single responsibility and makes the core logic straightforward to unit test.
	- Unit tests cover individual filters, pipeline behaviour, file reading and end-to-end orchestration.

Note: The following were intentionally avoided to keep the solution meaningful:
	- Parallel processing (hard to preserves output order).
	- DI framework (constructor injection is sufficient).
	- Configuration files (unnecessary for the current requirements).
