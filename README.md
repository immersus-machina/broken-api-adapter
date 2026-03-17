# BrokenApiAdapter

An adapter layer over unreliable external APIs. Fetches data from upstream endpoints that may change without notice, maps it through explicit transformation logic, and returns a stable, clean contract for consumers.

## Architecture

```
External API  →  BrokenApiAdapter  →  Mapper  →  Contract Model
(unreliable)      (generic HTTP)      (per-endpoint)   (stable, never changes)
```

### Key components

| Layer | Location | Purpose |
|-------|----------|---------|
| **Adapter** | `Adapter/` | Generic HTTP adapter — takes a URL and an `IMapper<TRaw, TClean>`, returns clean data |
| **External models** | `Models/External/` | Mirror the upstream API shape. These **will** change when the API drifts |
| **Contract models** | `Models/Contract/` | The stable output contract. These **must not** change |
| **Mappers** | `Mapping/` | Transform from external model to contract model. Updated when the external API changes |

### Adding a new endpoint

1. Create an external model in `Models/External/`
2. Create a mapper in `Mapping/` implementing `IMapper<TExternal, TContract>`
3. Register the mapper in `Program.cs`
4. Add the endpoint call in a controller using `IBrokenApiAdapter`
5. Add a schema reference entry (see below)

## Schema Drift Detection

Automated pipelines detect when an external API's data structure changes and attempt to fix the mapping.

### How it works

```
┌─────────────────────┐     drift?     ┌──────────────────────┐
│  Scheduled workflow  │──────────────→ │  Commit new schema   │
│  (daily cron)        │               │  to schema-drift/*   │
│                      │               │  branch              │
│  curl + jq extract   │    no drift   └──────────┬───────────┘
│  compare to stored   │──→ exit                   │
│  schema reference    │                           ▼
└─────────────────────┘               ┌──────────────────────┐
                                      │  Fix-mapping workflow │
                                      │  triggers on schema   │
                                      │  file changes         │
                                      │                       │
                                      │  Claude Code updates  │
                                      │  external model +     │
                                      │  mapper only          │
                                      │                       │
                                      │  Opens a PR for       │
                                      │  human review         │
                                      └───────────────────────┘
```

### Schema references

Each external endpoint has a corresponding schema file in `SchemaReferences/`:

- **`manifest.json`** — maps each schema file to its external model, mapper, and contract model
- **`*.schema.json`** — the stored JSON structure (property names + types) of the external API response

When adding a new endpoint, add an entry to `manifest.json`:

```json
{
  "schema": "my-new-endpoint.schema.json",
  "url": "https://example.com/api/data",
  "externalModel": "Models/External/MyExternalModel.cs",
  "mapper": "Mapping/MyMapper.cs",
  "contractModel": "Models/Contract/MyContractModel.cs"
}
```

Then generate the initial schema reference by running:

```bash
.github/scripts/extract-schema.sh "https://example.com/api/data" > SchemaReferences/my-new-endpoint.schema.json
```

### Workflows

| Workflow | Trigger | What it does |
|----------|---------|-------------|
| `schema-drift-check.yml` | Daily cron + manual | Fetches each endpoint, compares schema, commits changes if drifted |
| `fix-mapping.yml` | Schema file changes | Runs Claude Code to update the affected external model and mapper, opens a PR |

### Required secrets

- `ANTHROPIC_API_KEY` — for the Claude Code fix-mapping workflow
