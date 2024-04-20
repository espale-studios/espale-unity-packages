"""
Running this code will create a csv file called
espale_localization_sheet.csv where the first column
contains the keys and the following columns contain
the localized values. After editing the csv run the 
"lng_file_generator.py" to turn it back to seperate
language files. Note that if you add a new language
to the game after creating the csv the conversion will
not work.
"""

# === IMPORTS ===
import os
import csv

# === CONSTANTS ===
OUTPUT_FILE_NAME = "espale_localization_sheet.csv"
KEYS_FILE = "_keys.txt"
LNG_NAMES_FILE = "_lng_names.txt"
LINE_BREAK = "<line_break>"

# === VARIABLES ===
keys = []
languages = []
language_names = []

# === FUNCTIONS ===

# Strips each item in a list and replaces line breaks with <line_break>
def get_striped_list(list_to_strip: list) -> list:
    # strip to remove /n's.
    return [i.strip().replace(LINE_BREAK, "\n") for i in list_to_strip]

# Generating row dict from index
def get_row_dict(index: int) -> dict:
    row_dict = {"Keys": keys[index]}
    for language, language_name in zip(languages, language_names):
        row_dict[language_name] = language[index]

    return row_dict

# === MAIN ===
if __name__ == "__main__":
    # Converting files to lists
    for file in os.listdir():
        # keys file
        if file == KEYS_FILE:
            with open(file, "r", encoding="utf-8") as f:
                keys = get_striped_list(f.readlines())

        # language file
        if file.endswith(".txt") and not file.startswith("_"):
            with open(file, "r", encoding="utf-8") as f:
                lines = f.readlines()
                last_line = lines[len(f.readlines()) - 1]
                if last_line == "\n":
                    lines.append("")

                languages.append(get_striped_list(lines))
                language_names.append(file.replace(".txt", ""))

    # Creating the csv
    with open(OUTPUT_FILE_NAME, "w", newline="", encoding="utf-8") as f:
        fieldnames = ["Keys"] + [lng_name for lng_name in language_names]
        writer = csv.DictWriter(f, fieldnames=fieldnames, delimiter=",")

        writer.writeheader()  # writing the field names
        for i in range(len(keys)):
            writer.writerow(get_row_dict(i))

    print(f"\nGenerated {OUTPUT_FILE_NAME} with {len(keys)} keys.")
