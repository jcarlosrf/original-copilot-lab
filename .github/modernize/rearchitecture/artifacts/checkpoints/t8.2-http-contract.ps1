param(
    [string]$BaseUrl = 'http://127.0.0.1:5201'
)

$ErrorActionPreference = 'Stop'
$script:Assertions = [System.Collections.Generic.List[object]]::new()
$repoRoot = (Resolve-Path (Join-Path $PSScriptRoot '..\..\..\..\..')).Path

function Add-Assertion {
    param(
        [string]$Name,
        [bool]$Passed,
        [string]$Evidence = ''
    )

    $script:Assertions.Add([pscustomobject]@{
        Name = $Name
        Passed = $Passed
        Evidence = $Evidence
    })
}

function Get-ContractResponse {
    param([string]$Path)

    $response = Invoke-WebRequest -Uri ($BaseUrl + $Path) -UseBasicParsing -ErrorAction Stop
    [pscustomobject]@{
        Status = [int]$response.StatusCode
        Type = ($response.Headers['Content-Type'] -split ';')[0]
        Body = $response.Content
    }
}

$routes = [ordered]@{
    '/' = 'Welcome to the The Daily Harvest!'
    '/products' = 'Our Products'
    '/cart' = 'Your Cart'
    '/login' = 'Admin Login'
    '/admin' = 'Welcome to the admin portal.'
}
$pages = @{}

foreach ($route in $routes.Keys) {
    try {
        $response = Get-ContractResponse $route
        $pages[$route] = $response.Body
        Add-Assertion "route $route returns HTTP 200" ($response.Status -eq 200) "status=$($response.Status)"
        Add-Assertion "route $route contains expected heading" ($response.Body.Contains($routes[$route])) "heading=$($routes[$route])"
    }
    catch {
        Add-Assertion "route $route request completes" $false $_.Exception.Message
    }
}

$navigationTargets = @('/', '/products', '/cart', '/login')
foreach ($route in $routes.Keys) {
    if ($pages.ContainsKey($route)) {
        $nav = [regex]::Match($pages[$route], '<nav\b[^>]*>([\s\S]*?)</nav>')
        $targets = @([regex]::Matches($nav.Groups[1].Value, '<a\b[^>]*href="([^"]+)"') | ForEach-Object { $_.Groups[1].Value })
        foreach ($target in $navigationTargets) {
            Add-Assertion "navigation $route includes $target" ($nav.Success -and $targets -contains $target) "targets=$($targets -join ',')"
        }

        Add-Assertion "route $route renders shared header and footer" ($pages[$route].Contains('The Daily Harvest') -and $pages[$route].Contains('All rights reserved.')) ''
    }
}

Add-Assertion 'admin storefront link targets /' ($pages['/admin'] -match '<a class="storefront-button" href="/">Back to Storefront</a>') ''
Add-Assertion 'cart begins empty without checkout action' ($pages['/cart'].Contains('Your cart is empty.') -and $pages['/cart'] -notmatch '(?i)checkout|place order|order confirmation') ''
Add-Assertion 'login exposes username/password and no fabricated result' ($pages['/login'].Contains('name="username"') -and $pages['/login'].Contains('name="password"') -and $pages['/login'] -notmatch '(?i)invalid credentials|login successful|authenticated') ''
Add-Assertion 'admin shows passive no-sale state and controls' ($pages['/admin'].Contains('Set Sale Percent (% off for all items):') -and $pages['/admin'].Contains('value="0"') -and $pages['/admin'].Contains('No sale active.') -and $pages['/admin'].Contains('>Submit</button>') -and $pages['/admin'].Contains('>End Sale</button>')) ''

$stylesheets = [regex]::Matches($pages['/'], '<link\b[^>]*rel="stylesheet"[^>]*href="([^"]+)"')
Add-Assertion 'home references three stylesheets' ($stylesheets.Count -eq 3) "count=$($stylesheets.Count)"
foreach ($match in $stylesheets) {
    $assetPath = '/' + $match.Groups[1].Value.TrimStart('/')
    try {
        $asset = Get-ContractResponse $assetPath
        Add-Assertion "stylesheet $assetPath returns HTTP 200" ($asset.Status -eq 200) "status=$($asset.Status)"
        Add-Assertion "stylesheet $assetPath is CSS" ($asset.Type -eq 'text/css') "content-type=$($asset.Type)"
    }
    catch {
        Add-Assertion "stylesheet $assetPath request completes" $false $_.Exception.Message
    }
}

$catalogFiles = @('apple.json', 'grapes.json', 'orange.json', 'pear.json')
$catalog = [System.Collections.Generic.List[object]]::new()
foreach ($file in $catalogFiles) {
    try {
        $asset = Get-ContractResponse "/products/$file"
        Add-Assertion "catalog $file returns HTTP 200" ($asset.Status -eq 200) "status=$($asset.Status)"
        Add-Assertion "catalog $file is JSON" ($asset.Type -eq 'application/json') "content-type=$($asset.Type)"
        $product = $asset.Body | ConvertFrom-Json
        $catalog.Add($product)

        $sourcePath = Join-Path $repoRoot "eCommApp\public\products\$file"
        $source = Get-Content $sourcePath -Raw | ConvertFrom-Json
        foreach ($property in @('id', 'name', 'description', 'price', 'image', 'category', 'inStock')) {
            Add-Assertion "catalog $file $property matches React fixture" ($product.$property -eq $source.$property) "value=$($product.$property)"
        }
    }
    catch {
        Add-Assertion "catalog $file parses and matches fixture" $false $_.Exception.Message
    }
}

$expectedNames = @('Apple', 'Grapes', 'Orange', 'Pear')
$actualNames = @($catalog | ForEach-Object { $_.name })
Add-Assertion 'catalog order is Apple, Grapes, Orange, Pear' (($actualNames -join ',') -eq ($expectedNames -join ',')) "actual=$($actualNames -join ',')"
$orange = $catalog | Where-Object name -eq 'Orange' | Select-Object -First 1
Add-Assertion 'Orange catalog item is unavailable' ($null -ne $orange -and $orange.inStock -eq $false) "inStock=$($orange.inStock)"

$productHtml = $pages['/products']
$renderedNames = @([regex]::Matches($productHtml, '<h3 class="product-name">(.*?)</h3>') | ForEach-Object { [System.Net.WebUtility]::HtmlDecode($_.Groups[1].Value) })
$cardCount = [regex]::Matches($productHtml, '<article class="product-card">').Count
Add-Assertion 'products page renders exactly four cards' ($cardCount -eq 4) "cards=$cardCount"
Add-Assertion 'products page preserves catalog order' (($renderedNames -join ',') -eq ($expectedNames -join ',')) "actual=$($renderedNames -join ',')"
Add-Assertion 'Orange renders disabled Out of Stock control' ($productHtml -match 'class="add-to-cart-btn disabled"[^>]*disabled[^>]*>\s*Out of Stock\s*</button>') ''

foreach ($product in $catalog) {
    $imagePath = "/products/productImages/$($product.image)"
    try {
        $image = Get-ContractResponse $imagePath
        Add-Assertion "image $imagePath returns HTTP 200" ($image.Status -eq 200) "status=$($image.Status)"
        Add-Assertion "image $imagePath is PNG" ($image.Type -eq 'image/png') "content-type=$($image.Type)"
        Add-Assertion "products page references $imagePath with product alt" ($productHtml.Contains("src=`"$imagePath`"") -and $productHtml.Contains("alt=`"$($product.name)`"")) ''
    }
    catch {
        Add-Assertion "image $imagePath request completes" $false $_.Exception.Message
    }
}

$failed = @($script:Assertions | Where-Object { -not $_.Passed })
$script:Assertions | ForEach-Object {
    '{0} {1}: {2}' -f ($(if ($_.Passed) { 'PASS' } else { 'FAIL' })), $_.Name, $_.Evidence
}
"ASSERTIONS: passed=$($script:Assertions.Count - $failed.Count) failed=$($failed.Count) total=$($script:Assertions.Count)"

if ($failed.Count -gt 0) {
    exit 1
}