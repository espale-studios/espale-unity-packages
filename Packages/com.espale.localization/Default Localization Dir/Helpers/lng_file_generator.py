# === IMPORTS ===
import csv

# === CONSTANTS ===
LINE_BREAK = "<line_break>"
KEYS_FILE = "_keys.txt"

# === VARIABLES ===
keys = []
languages = []
language_names = []

# === FUNCTIONS ===
def convert_vals_to_lines(vals: list) -> list:
    return [val + "\n" for i, val in enumerate(vals)]

# === MAIN ===
if __name__ == "__main__":
    file_name = input("enter file name without the .csv (leave empty for espale_localization_sheet): ") + ".csv"
    if file_name == ".csv":
        file_name = "espale_localization_sheet.csv"

    # Read the csv
    with open(file_name, "r", encoding="utf-8") as f:
        for i, line in enumerate(list(csv.reader(f, skipinitialspace=True))):
            # Generate language and language names lists
            if i == 0:
                for j in range(1, len(line)):
                    language_names.append(line[j].strip())
                    languages.append([])
                continue

            keys.append(line.pop(0).strip())
            for j, value in enumerate(line):
                languages[j].append(value.strip().replace("\n", LINE_BREAK))

    # Sort all the lists by the keys
    for i in range(len(languages)):
        _, languages[i] = zip(*sorted(zip(keys, languages[i])))

    keys.sort()

    # Write the keys
    with open(KEYS_FILE, "w", encoding="utf-8") as f:
        f.writelines(convert_vals_to_lines(keys))

    # Write the languages
    for language, language_name in zip(languages, language_names):
        with open(f"{language_name}.txt", "w", encoding="utf-8") as f:
            f.writelines(convert_vals_to_lines(language))

    print(f"\nGenerated Language Files from {file_name} with {len(keys)} keys.")  
