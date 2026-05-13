# Z-index a pozicování uloha

# Praktické cvičení: Layout blogu s modálem (30 minut)

**Cíl:** Vytvořit jednoduchou strukturu, kde vyzkoušíš fixní menu, sticky nadpis, absolutní štítek a modální okno.

### Zadání:

Vytvoř HTML a CSS pro následující scénář:

1. **Hlavička (Header):** Bude `sticky`, při scrollování se přilepí k hornímu okraji (`top: 0`) a bude mít barvu pozadí, aby byl text pod ní nečitelný.
2. **Článek (Article):**
    - Obsahuje dlouhý text (lorem ipsum), aby šlo scrollovat.
    - Uvnitř textu bude **obrázek (div)**, který bude obtékán textem zprava (`float: left`).
3. **Karta autora (Author Card):**
    - Umístěna uvnitř článku.
    - Musí mít **štítek "TOP AUTOR"**, který bude v pravém horním rohu karty (použij kombinaci `relative` + `absolute`).
4. **Modální okno (Cookie lišta/Modal):**
    - Bude fixně umístěno v pravém dolním rohu obrazovky.
    - Musí být vždy vidět nad ostatním obsahem (`z-index`).

### Startovací kód (HTML):

Zkopíruj si tento kód a doplň CSS do značky `<style>`.

```jsx
<!DOCTYPE html>
<html lang="cs">
<head>
    <meta charset="UTF-8">
    <title>Cvičení Pozicování</title>
    <style>
        body { margin: 0; font-family: sans-serif; height: 200vh; /* Prodloužení stránky pro scroll */ }
        
        /* 1. STICKY HEADER */
        header {
            background-color: #333;
            color: white;
            padding: 20px;
            /* ZDE DOPLŇ: position sticky, top, z-index (aby byl nad textem) */
        }

        .container { max-width: 800px; margin: 0 auto; padding: 20px; border: 1px solid #ccc; }

        /* 2. FLOAT OBRÁZEK */
        .float-box {
            width: 150px;
            height: 150px;
            background-color: lightblue;
            margin-right: 20px;
            margin-bottom: 10px;
            /* ZDE DOPLŇ: float */
        }

        /* 3. RELATIVE + ABSOLUTE (Karta autora) */
        .author-card {
            margin-top: 50px;
            border: 2px solid #333;
            padding: 20px;
            background: #f9f9f9;
            /* ZDE DOPLŇ: position relative (rodič) */
        }

        .badge {
            background-color: red;
            color: white;
            padding: 5px 10px;
            font-weight: bold;
            font-size: 12px;
            /* ZDE DOPLŇ: position absolute, umístění do pravého horního rohu (top, right) */
        }

        /* 4. FIXED MODAL (Cookie/Info) */
        .modal {
            background-color: #ffeb3b;
            padding: 20px;
            border: 1px solid orange;
            width: 200px;
            box-shadow: 0 4px 10px rgba(0,0,0,0.3);
            /* ZDE DOPLŇ: position fixed, umístění vpravo dole (bottom, right), z-index */
        }
    </style>
</head>
<body>

    <header>Můj Sticky Blog (Scroluj dolů)</header>

    <div class="container">
        <h1>Hlavní nadpis článku</h1>
        
        <div class="float-box">Obrázek (float)</div>
        
        <p>Lorem ipsum dolor sit amet, consectetur adipiscing elit. Vestibulum in finibus ex. Nulla facilisi. Suspendisse potenti. Sed egestas, ante et vulputate volutpat, eros pede semper est, vitae luctus metus libero eu augue. Morbi purus libero, faucibus adipiscing, commodo quis, gravida id, est.</p>
        <p>Sed lectus. Praesent elementum hendrerit tortor. Sed semper lorem at felis. Vestibulum volutpat, lacus a ultrices sagittis, mi neque euismod dui, eu pulvinar nunc sapien ornare nisl. Phasellus pede arcu, dapibus eu, fermentum et, dapibus sed, urna.</p>
        
        <div class="author-card">
            <div class="badge">TOP AUTOR</div>
            <h3>Jan Novák</h3>
            <p>Expert na CSS pozicování.</p>
        </div>

        <p>Doplňkový text pro dlouhé scrolování... Lorem ipsum dolor sit amet.</p>
        <p>Další odstavce pro test sticky headeru...</p>
        <p>Další odstavce pro test sticky headeru...</p>
        <p>Další odstavce pro test sticky headeru...</p>
    </div>

    <div class="modal">
        <strong>Upozornění:</strong><br>
        Toto okno je fixní a zůstává zde i při scrollování.
    </div>

</body>
</html>
```

Řešení (CSS část): 

```jsx
/* 1. STICKY HEADER */
        header {
            position: sticky;
            top: 0;
            z-index: 67; /* Aby překryl obsah při scrollování */
        }

        /* 2. FLOAT OBRÁZEK */
        .float-box {
            float: left;
        }

        /* 3. RELATIVE + ABSOLUTE */
        .author-card {
            position: relative; /* Definuje hranice pro absolutní dítě */
        }

        .badge {
            position: absolute;
            top: -10px; /* Mírný přesah ven */
            right: -10px;
        }

        /* 4. FIXED MODAL */
        .modal {
            position: fixed;
            bottom: 20px;
            right: 20px;
            z-index: 69; /* Nejvyšší priorita */
        }
```