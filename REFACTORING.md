# Refactoring: naming pass

Started 2026-08-28. Tick items off as you go.

Suggested order, so you don't touch the same line four times:

1. casing
2. parameter + local names
3. named tuple elements / `Position` type
4. getters → properties

---

## 0. Read first

- [ ] C# identifier naming conventions — https://learn.microsoft.com/en-us/dotnet/csharp/fundamentals/coding-style/identifier-names

Four different rules: methods and public members `PascalCase`, locals and parameters
`camelCase`, private fields `_camelCase`. You're currently using `snake_case` for all four.

---

## Map.cs

| Now                          | Rename to            | Why                                                                         |
|------------------------------|----------------------|-----------------------------------------------------------------------------|
| `map_lobby_path`             | `_lobbyMapPath`      | private field → `_camelCase`; adjective first, thing last                   |
| `xy` (the size)              | `_width` + `_height` | it's two lengths, not a point — kills `.Item1` and the `HasValue` dance     |
| `map` (the field)            | `_rows`              | inside a class called `Map`, `map` says nothing; it's an array of text rows |
| `loadMapFile`                | `LoadFromFile`       | method → `PascalCase`; drop `Map`, `map.LoadMapFile()` stutters             |
| `moveEntity`                 | `MoveEntity`         | casing only, name is good                                                   |
| `entity_position_xy_old`     | `from`               | movement has a from and a to                                                |
| `entity_position_xy_new`     | `to`                 | "                                                                           |
| `entity_icon`                | `glyph`              | your own design notes say "glyph" — use one word per concept                |
| `xy_new` / `xy_old` (locals) | **delete both**      | they only rename the parameters; fix the parameters instead                 |
| `sb`                         | `fromRow`            | the map row you're erasing the entity from                                  |
| `sb2`                        | `toRow`              | the row you're drawing into                                                 |
| `'.'` (literal)              | `FloorGlyph` const   | a magic character mid-method is an unnamed concept                          |
| `getMap()`                   | `Rows` (property)    | properties are named like the thing, not like an action                     |
| `getXY()`                    | `Width` / `Height`   | two properties instead of one tuple                                         |

## Entity.cs

| Now                   | Rename to  | Why                                                       |
|-----------------------|------------|-----------------------------------------------------------|
| `xy`                  | `Position` | public → `PascalCase`; and that's the concept's real name |
| `icon`                | `Glyph`    | consistency with `Map`                                    |
| `position_xy` (param) | `position` | the `_xy` suffix is the type's job, not the name's        |

## Program.cs

| Now                   | Rename to       | Why                                                           |
|-----------------------|-----------------|---------------------------------------------------------------|
| `map` (static field)  | `_map`          | a field named exactly like the type `Map` will bite you       |
| `map_xy`              | **delete**      | use `map.Width` / `map.Height` at the comparison              |
| `default_player_xy`   | `startPosition` | "default" says where it came from; "start" says what it means |
| `current_player_xy`   | **delete**      | see below                                                     |
| `xy_new`              | `target`        | where the player is *trying* to go                            |
| `ki`                  | `key`           | two letters save nothing and cost a lookup                    |
| `printCurrentMap`     | `Render`        | it draws the whole map; `Current` adds nothing                |
| `current_map` (local) | **delete**      | loop over `map.Rows` directly                                 |

**`current_player_xy` can't be renamed honestly.** `player.Position` already means the same
thing, and the two disagree after the first keypress. Two variables for one fact is how that
happens. Keep the one that belongs to the player.

---

## Properties: the shapes

- `public int Total { get; }` — storage, assignable **only** in the constructor. Goes stale
  if derived from something that changes later.
- `public int Total { get; private set; }` — real setter, just hidden. Class can rewrite anytime.
- `public int Total => _total;` — a method, no storage, recomputed on every read.
- `private readonly int _limit;` — field, write-locked after the constructor.

Default accessibility of a class member is **private**. No modifier = invisible to `Program.cs`.

**For width and height: derive them from the rows.** They aren't independent facts. A derived
property has no setter to guard and can't disagree with the array — and it deletes the
nullable / `HasValue` business, since an unloaded map has no rows to measure.

**Caveat for `Rows`:** read-only protects the *reference*, not the contents. A caller can still
write `map.Rows[0] = "..."`. Not today's problem — but it's why `IReadOnlyList<T>` exists.
Search terms: `C# ReadOnlyCollection<T>`, `C# indexer`, `C# defensive copy property`.

Docs:
- https://learn.microsoft.com/en-us/dotnet/csharp/properties
- https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/readonly
- https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/access-modifiers
- sharplab.io — paste a class, see what the compiler actually generated

---

## `Item1` / `Item2`

You can't name your way out of `xy_new.Item1`. It's a type problem.

- [ ] **Cheap:** name the tuple elements in the type — `(int X, int Y)`, then write `.X` / `.Y`.
      Search `C# named tuple elements`.
- [ ] **The real fix, later:** a `Position` type of your own. Player has one, monsters will have
      one, the map bounds-checks them — it's a real concept in the game. Design discussion, not
      a rename. Do it after the renaming pass.

---

## Naming heuristics (the part that transfers)

1. Say what it **is**, not how it's shaped. `Position`, not `xy` — the type carries the shape.
2. Don't repeat your context. In `Map`, nothing needs the word `map`. In `MoveEntity`, nothing
   needs the word `entity`.
3. A name must stay true. If it can go stale, delete the variable or fix the code.
4. `x`, `x2` means you skipped naming. A `2` suffix is the prompt to ask what actually
   distinguishes the two.
5. One word per concept, everywhere. Not `icon` here and `glyph` there.

---

## Not naming — separate pass, don't mix in

- [ ] `Map.cs:7` — hardcoded absolute path, only works on your machine
- [ ] the lazy `loadMapFile` call repeated in three methods belongs in a constructor
