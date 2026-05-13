"""
Konverze Markdown rozboru na PDF pomocí fpdf2.
Použití: python md_to_pdf.py
Převede všechny .md soubory v aktuálním adresáři do PDF.
"""

import os
import re
from fpdf import FPDF

ARIAL = "C:/Windows/Fonts/arial.ttf"
ARIAL_BOLD = "C:/Windows/Fonts/arialbd.ttf"
ARIAL_ITALIC = "C:/Windows/Fonts/ariali.ttf"
ARIAL_BOLD_ITALIC = "C:/Windows/Fonts/arialbi.ttf"
SEGOE_EMOJI = "C:/Windows/Fonts/seguiemj.ttf"  # fallback pro emoji glyphy (✅, 📚 atd.)

MARGIN_LEFT = 15
MARGIN_RIGHT = 15
MARGIN_TOP = 15
PAGE_WIDTH = 210  # A4


class RozborPDF(FPDF):
    def __init__(self):
        super().__init__()
        self.add_font("Arial", style="", fname=ARIAL)
        self.add_font("Arial", style="B", fname=ARIAL_BOLD)
        self.add_font("Arial", style="I", fname=ARIAL_ITALIC)
        self.add_font("Arial", style="BI", fname=ARIAL_BOLD_ITALIC)
        if os.path.exists(SEGOE_EMOJI):
            self.add_font("SegoeEmoji", style="", fname=SEGOE_EMOJI)
            self.set_fallback_fonts(["SegoeEmoji"])
        self.set_margins(MARGIN_LEFT, MARGIN_TOP, MARGIN_RIGHT)
        self.set_auto_page_break(auto=True, margin=15)
        self.set_font("Arial", size=11)

    def header(self):
        pass

    def footer(self):
        self.set_y(-12)
        self.set_font("Arial", size=8)
        self.set_text_color(128, 128, 128)
        self.cell(0, 5, f"Strana {self.page_no()}", align="C")
        self.set_text_color(0, 0, 0)


def strip_md_inline(text):
    """Odstraní inline markdown (bold, italic, code) a vrátí čistý text s tučnými segmenty."""
    # Vrátí seznam segmentů: (text, bold, italic)
    segments = []
    i = 0
    while i < len(text):
        # Bold italic ***text*** nebo ___
        m = re.match(r'\*\*\*(.+?)\*\*\*|___(.+?)___', text[i:])
        if m:
            segments.append((m.group(1) or m.group(2), True, True))
            i += m.end()
            continue
        # Bold **text**
        m = re.match(r'\*\*(.+?)\*\*|__(.+?)__', text[i:])
        if m:
            segments.append((m.group(1) or m.group(2), True, False))
            i += m.end()
            continue
        # Italic *text*
        m = re.match(r'\*(.+?)\*|_(.+?)_', text[i:])
        if m:
            segments.append((m.group(1) or m.group(2), False, True))
            i += m.end()
            continue
        # Code `text`
        m = re.match(r'`(.+?)`', text[i:])
        if m:
            segments.append((m.group(1), False, False))
            i += m.end()
            continue
        # Normal char
        if segments and not segments[-1][1] and not segments[-1][2]:
            segments[-1] = (segments[-1][0] + text[i], False, False)
        else:
            segments.append((text[i], False, False))
        i += 1
    return segments


def write_inline(pdf, text, base_size=11):
    """Zapíše řádek textu s respektováním bold/italic markdown."""
    segments = strip_md_inline(text)
    for seg_text, bold, italic in segments:
        if not seg_text:
            continue
        style = ""
        if bold:
            style += "B"
        if italic:
            style += "I"
        pdf.set_font("Arial", style=style, size=base_size)
        pdf.write(6, seg_text)
    pdf.set_font("Arial", size=base_size)


def render_markdown(pdf, lines):
    """Zpracuje řádky markdown a vykreslí do PDF."""
    effective_width = PAGE_WIDTH - MARGIN_LEFT - MARGIN_RIGHT
    i = 0
    while i < len(lines):
        line = lines[i]
        raw = line.rstrip()

        # Prázdný řádek
        if raw == "":
            pdf.ln(3)
            i += 1
            continue

        # Horizontální čára
        if re.match(r'^---+$', raw):
            pdf.set_draw_color(180, 180, 180)
            pdf.set_line_width(0.3)
            y = pdf.get_y()
            pdf.line(MARGIN_LEFT, y, PAGE_WIDTH - MARGIN_RIGHT, y)
            pdf.ln(4)
            i += 1
            continue

        # H1 #
        m = re.match(r'^# (.+)$', raw)
        if m:
            pdf.ln(2)
            pdf.set_font("Arial", style="B", size=16)
            pdf.set_fill_color(30, 30, 30)
            pdf.set_text_color(255, 255, 255)
            pdf.set_x(MARGIN_LEFT)
            pdf.multi_cell(effective_width, 8, m.group(1), fill=True, align="C")
            pdf.set_text_color(0, 0, 0)
            pdf.ln(4)
            i += 1
            continue

        # H2 ##
        m = re.match(r'^## (.+)$', raw)
        if m:
            pdf.ln(3)
            pdf.set_font("Arial", style="B", size=14)
            pdf.set_fill_color(50, 80, 140)
            pdf.set_text_color(255, 255, 255)
            pdf.cell(effective_width, 8, m.group(1), fill=True, ln=True)
            pdf.set_text_color(0, 0, 0)
            pdf.ln(2)
            i += 1
            continue

        # H3 ###
        m = re.match(r'^### (.+)$', raw)
        if m:
            pdf.ln(2)
            pdf.set_font("Arial", style="B", size=12)
            pdf.set_text_color(50, 80, 140)
            heading_text = m.group(1)
            pdf.cell(0, 7, heading_text, ln=True)
            pdf.set_text_color(0, 0, 0)
            # Podtržení
            y = pdf.get_y()
            pdf.set_draw_color(50, 80, 140)
            pdf.set_line_width(0.4)
            pdf.line(MARGIN_LEFT, y, PAGE_WIDTH - MARGIN_RIGHT, y)
            pdf.ln(2)
            i += 1
            continue

        # H4 ####
        m = re.match(r'^#### (.+)$', raw)
        if m:
            pdf.ln(1)
            pdf.set_font("Arial", style="B", size=11)
            pdf.cell(0, 6, m.group(1), ln=True)
            pdf.ln(1)
            i += 1
            continue

        # Tabulka |...|
        if raw.startswith("|"):
            # Shromáždi všechny řádky tabulky
            table_lines = []
            while i < len(lines) and lines[i].strip().startswith("|"):
                table_lines.append(lines[i].strip())
                i += 1
            render_table(pdf, table_lines, effective_width)
            pdf.ln(3)
            continue

        # Odrážka - nebo *
        m = re.match(r'^(\s*)([-*+]) (.+)$', raw)
        if m:
            indent = len(m.group(1)) // 2
            bullet_text = m.group(3)
            x_start = MARGIN_LEFT + indent * 5
            pdf.set_x(x_start)
            pdf.set_font("Arial", size=10)
            bullet = "\u2022" if indent == 0 else "\u2013"
            pdf.cell(5, 6, bullet)
            pdf.set_x(x_start + 5)
            # inline text s formátováním
            write_inline_multiline(pdf, bullet_text, x_start + 5, effective_width - indent * 5 - 5, 10)
            pdf.ln(0)
            i += 1
            continue

        # Číslovaná odrážka 1.
        m = re.match(r'^(\s*)(\d+)\. (.+)$', raw)
        if m:
            indent = len(m.group(1)) // 2
            num = m.group(2)
            text = m.group(3)
            x_start = MARGIN_LEFT + indent * 5
            pdf.set_x(x_start)
            pdf.set_font("Arial", style="B", size=10)
            pdf.cell(8, 6, f"{num}.")
            pdf.set_x(x_start + 8)
            write_inline_multiline(pdf, text, x_start + 8, effective_width - indent * 5 - 8, 10)
            pdf.ln(0)
            i += 1
            continue

        # Normální odstavec
        pdf.set_x(MARGIN_LEFT)
        write_inline_multiline(pdf, raw, MARGIN_LEFT, effective_width, 11)
        pdf.ln(1)
        i += 1


def write_inline_multiline(pdf, text, x_start, width, size):
    """Zapíše text s inline formátováním na více řádcích."""
    segments = strip_md_inline(text)
    pdf.set_x(x_start)
    for seg_text, bold, italic in segments:
        if not seg_text:
            continue
        style = ""
        if bold:
            style += "B"
        if italic:
            style += "I"
        pdf.set_font("Arial", style=style, size=size)
        # Použij multi_cell pouze pro poslední segment nebo pokud je segment velký
        # Jinak write() pro zachování formátování inline
        pdf.write(6, seg_text)
    pdf.ln(6)
    pdf.set_font("Arial", size=size)


def strip_md_table_cell(text):
    """Odstraní inline markdown z buňky tabulky."""
    text = re.sub(r'\*\*(.+?)\*\*', r'\1', text)
    text = re.sub(r'\*(.+?)\*', r'\1', text)
    text = re.sub(r'__(.+?)__', r'\1', text)
    text = re.sub(r'_(.+?)_', r'\1', text)
    return text


def wrap_text_lines(pdf, text, width):
    """Rozdělí text do řádků tak, aby se vešel do dané šířky."""
    text = " ".join(str(text).split())
    if not text:
        return [""]

    words = text.split(" ")
    lines = []
    current = ""

    for word in words:
        candidate = word if not current else f"{current} {word}"
        if pdf.get_string_width(candidate) <= width:
            current = candidate
            continue

        if current:
            lines.append(current)
            current = ""

        if pdf.get_string_width(word) > width:
            chunk = ""
            for ch in word:
                test = chunk + ch
                if chunk and pdf.get_string_width(test) > width:
                    lines.append(chunk)
                    chunk = ch
                else:
                    chunk = test
            current = chunk
        else:
            current = word

    if current:
        lines.append(current)

    return lines or [""]


def compute_col_widths(headers, rows, effective_width):
    """Spočítá šířky sloupců podle obsahu místo rovnoměrného dělení."""
    n_cols = len(headers)
    lengths = [0] * n_cols

    for ci, header in enumerate(headers):
        lengths[ci] = max(lengths[ci], min(len(header), 40))

    for row in rows:
        for ci in range(n_cols):
            cell = row[ci] if ci < len(row) else ""
            lengths[ci] = max(lengths[ci], min(len(strip_md_table_cell(cell)), 60))

    weights = [max(12, l) for l in lengths]
    total = sum(weights)
    widths = [effective_width * w / total for w in weights]

    min_width = 22
    deficit = 0
    for i, width in enumerate(widths):
        if width < min_width:
            deficit += min_width - width
            widths[i] = min_width

    if deficit > 0:
        adjustable = [i for i, width in enumerate(widths) if width > min_width]
        while deficit > 0.01 and adjustable:
            share = deficit / len(adjustable)
            next_adjustable = []
            for i in adjustable:
                reducible = widths[i] - min_width
                reduction = min(reducible, share)
                widths[i] -= reduction
                deficit -= reduction
                if widths[i] - min_width > 0.01:
                    next_adjustable.append(i)
            adjustable = next_adjustable

    return widths


def render_table(pdf, table_lines, effective_width):
    """Vykreslí markdown tabulku s podporou víceřádkových buněk."""
    if len(table_lines) < 2:
        return

    header_line = table_lines[0]
    data_lines = [l for l in table_lines[2:] if l.strip()]

    def parse_row(line):
        cells = [c.strip() for c in line.split("|")]
        return [c for c in cells if c != ""]

    headers = parse_row(header_line)
    rows = [parse_row(l) for l in data_lines]

    if not headers:
        return

    n_cols = len(headers)
    line_height = 5
    padding = 1.5
    col_widths = compute_col_widths(headers, rows, effective_width)

    def render_row(cells, fill_color, text_color, font_style="", font_size=8):
        pdf.set_font("Arial", style=font_style, size=font_size)
        pdf.set_text_color(*text_color)

        wrapped_cells = []
        row_height = line_height
        for ci in range(n_cols):
            cell = cells[ci] if ci < len(cells) else ""
            plain = strip_md_table_cell(cell)
            inner_width = max(5, col_widths[ci] - 2 * padding)
            wrapped = wrap_text_lines(pdf, plain, inner_width)
            wrapped_cells.append(wrapped)
            row_height = max(row_height, len(wrapped) * line_height + 2 * padding)

        start_y = pdf.get_y()
        if start_y + row_height > pdf.h - pdf.b_margin:
            pdf.add_page()
            start_y = pdf.get_y()

        x = MARGIN_LEFT
        for ci in range(n_cols):
            width = col_widths[ci]
            pdf.set_fill_color(*fill_color)
            pdf.rect(x, start_y, width, row_height, style="DF")
            pdf.set_xy(x + padding, start_y + padding)
            for line in wrapped_cells[ci]:
                pdf.cell(width - 2 * padding, line_height, line, ln=True)
                pdf.set_x(x + padding)
            x += width

        pdf.set_xy(MARGIN_LEFT, start_y + row_height)

    render_row(headers, fill_color=(50, 80, 140), text_color=(255, 255, 255), font_style="B", font_size=9)

    for idx, row in enumerate(rows):
        fill = (240, 244, 255) if idx % 2 == 0 else (255, 255, 255)
        render_row(row, fill_color=fill, text_color=(0, 0, 0), font_size=8)


def md_to_pdf(md_path, pdf_path):
    """Převede jeden MD soubor na PDF."""
    with open(md_path, "r", encoding="utf-8") as f:
        content = f.read()

    lines = content.split("\n")

    pdf = RozborPDF()
    pdf.add_page()
    render_markdown(pdf, lines)
    pdf.output(pdf_path)
    print(f"  OK: {os.path.basename(pdf_path)}")


def main():
    script_dir = os.path.dirname(os.path.abspath(__file__))
    pdf_dir = os.path.join(script_dir, "pdf")
    os.makedirs(pdf_dir, exist_ok=True)

    md_files = sorted([f for f in os.listdir(script_dir) if f.endswith(".md")])

    if not md_files:
        print("Žádné .md soubory nenalezeny.")
        return

    print(f"Konvertuji {len(md_files)} souboru do: {pdf_dir}\n")
    errors = []
    for md_file in md_files:
        md_path = os.path.join(script_dir, md_file)
        pdf_name = md_file.replace(".md", ".pdf")
        pdf_path = os.path.join(pdf_dir, pdf_name)
        try:
            md_to_pdf(md_path, pdf_path)
        except Exception as e:
            errors.append((md_file, str(e)))
            print(f"  CHYBA: {md_file} – {e}")

    print(f"\nHotovo. {len(md_files) - len(errors)}/{len(md_files)} souboru prevedeno.")
    if errors:
        print("Chyby:")
        for f, e in errors:
            print(f"  {f}: {e}")


if __name__ == "__main__":
    main()
