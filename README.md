
# Proyecto Unity - Comic Extendido - Sinapsis WOW

Este proyecto de Unity ha sido creado en la versión **2022.3.20f1** y utiliza el **Unity Test Framework** para realizar pruebas unitarias en **Play Mode**.

## Requisitos del Proyecto

Antes de comenzar, asegúrate de tener instalados los siguientes requisitos:

- **Unity Hub** (última versión)
- **Unity Editor 2022.3.20f1**
- **Git** (opcional, si trabajas con control de versiones)

## Instrucciones para Ejecutar el Proyecto

### 1. Descargar e Instalar Unity

1. Descarga e instala Unity Hub desde [aquí](https://unity.com/download).
2. Dentro de Unity Hub, instala la versión **2022.3.20f1** del editor de Unity:
   - Abre **Unity Hub**.
   - Ve a la pestaña **Installs**.
   - Haz clic en **Install Editor** y selecciona la versión **2022.3.20f1**.

### 2. Abrir el Proyecto

1. Abre **Unity Hub**.
2. Haz clic en **Add** para agregar el proyecto de Unity.
3. Navega a la carpeta donde has descargado/clonado el proyecto y selecciónala.
4. Una vez cargado, haz clic en **Open** para abrir el proyecto.

### 3. Ejecutar el Proyecto

1. Una vez abierto el proyecto en Unity, puedes ejecutar el juego presionando el botón **Play** en la parte superior del editor.
2. Si el juego tiene varias escenas, asegúrate de establecer la escena correcta como **Scene Principal**:
   - Navega a la ventana **Project**.
   - Abre la carpeta **Assets > Scenes** y selecciona la escena principal.
   - Haz clic derecho sobre la escena y selecciona **Set as Active Scene**.

## Ejecución de Pruebas en **Play Mode**

El proyecto incluye pruebas automatizadas en **Play Mode** que verifican el comportamiento de varias funciones, como la carga de escenas y la interacción del usuario.

### 1. Abrir el Test Runner

1. Abre **Test Runner** desde **Window > General > Test Runner**.
2. Selecciona la pestaña **Play Mode** en la parte superior.

### 2. Ejecutar las Pruebas en Play Mode

1. Haz clic en **Run All** para ejecutar todas las pruebas en **Play Mode**.
2. El **Test Runner** ejecutará las pruebas relacionadas con la carga de escenas y otros elementos que requieren la simulación en tiempo real.

### Pruebas en Play Mode

- **Pruebas del `SceneLoader`**: Validan el progreso de la carga de escenas asíncrona y aseguran que las escenas se carguen correctamente en tiempo real.
  
### Problemas Comunes con las Pruebas

1. **Escena No Encontrada**: Si se produce un error al intentar cargar una escena, asegúrate de que la escena esté incluida en la lista de escenas dentro de **Build Settings**:
   - Ve a **File > Build Settings**.
   - Arrastra la escena correspondiente a la lista de **Scenes in Build**.

## Contribuir al Proyecto

Si deseas contribuir al proyecto, sigue los siguientes pasos:

1. Haz un **fork** de este repositorio.
2. Crea una nueva rama para tu característica o corrección de errores: `git checkout -b feature/nueva-caracteristica`.
3. Haz los cambios necesarios y verifica que todas las pruebas pasen.
4. Crea un **pull request** para revisión.
