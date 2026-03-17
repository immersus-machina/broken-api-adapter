#!/usr/bin/env bash
# Extracts a deterministic JSON schema (property names + types) from a JSON array endpoint.
# Usage: ./extract-schema.sh <url>
#
# Takes the first element of the array response, extracts each property's name and
# JSON type, then outputs a sorted, stable schema representation.

set -euo pipefail

URL="${1:?Usage: extract-schema.sh <url>}"

curl -sf "$URL" \
  | jq -S '
    (.[0] // {})
    | to_entries
    | map({
        key: .key,
        value: { type: (.value | type) }
      })
    | sort_by(.key)
    | {
        properties: from_entries,
        propertyNames: [ .[].key ]
      }
  '
