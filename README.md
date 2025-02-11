# NUnitFrameworkDesign

Este proyecto es un framework de automatización de pruebas utilizando **NUnit** y **Selenium WebDriver** en **C#**.

## 📌 Características principales

- Implementación de **Page Object Model (POM)**.
- Uso de **NUnit** para gestión y ejecución de pruebas.
- Soporte para ejecución de pruebas en **paralelo**.
- Configuración mediante archivos **RunSettings**.
- Parametrización de pruebas con **TestCaseSource**.
- Reportes de ejecución con **Extent Reports**.

## 🚀 Instalación y Configuración

1. **Clonar el repositorio**
```sh
    git clone https://github.com/tu_usuario/NUnitFrameworkDesign.git
    cd NUnitFrameworkDesign
```

2. **Instalar dependencias necesarias**
```sh
    dotnet restore
```

3. **Configurar el archivo `appsettings.json`**

En la carpeta `data`, actualiza `appsettings.json` con los valores correctos:
```json
{
  "browser": "chrome",
  "Headless": false
}
```



## 🏃‍♂️ Ejecutar las Pruebas

Ejecutar **todas las pruebas**:
```sh
    dotnet test
```

Ejecutar pruebas **de una categoría específica**:
```sh
    dotnet test --filter Category=ErrorValidation
```

Ejecutar pruebas **con configuración personalizada**:
```sh
    dotnet test --settings NUnit.runsettings
```

## ⚡ Ejecución en Paralelo
Para ejecutar pruebas en paralelo, asegurarse de:
- Definir `[Parallelizable(ParallelScope.All)]` en las clases de prueba.
- Configurar `NumberOfTestWorkers` en el archivo `NUnit.runsettings`:

```xml
<RunSettings>
  <NUnit>
    <NumberOfTestWorkers>4</NumberOfTestWorkers>
  </NUnit>
</RunSettings>
```

Ejecutar en paralelo:
```sh
    dotnet test --settings NUnit.runsettings
```

---

📌 **Autor:** Tu Nombre  
📌 **Repositorio:** [GitHub](https://github.com/tu_usuario/NUnitFrameworkDesign)
