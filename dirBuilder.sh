#!/bin/sh

for file in *.csproj; do
    projectDir=$( printf "%s\n" "$file" | sed -e 's/.\{7\}$//' )
    mkdir -p "$projectDir"
    mv "$file" "$projectDir"
done

rm -- "$0"