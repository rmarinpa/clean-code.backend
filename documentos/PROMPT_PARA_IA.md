# Prompt para IA - Implementación Clean Architecture

## 🤖 **Prompt Principal**

```
Necesito que implementes una arquitectura Clean Code completa en .NET siguiendo las instrucciones detalladas del archivo adjunto "AI_ARCHITECTURE_INSTRUCTIONS.md".

**Parámetros del proyecto:**
- Nombre del proyecto: [NOMBRE_DEL_PROYECTO]
- Versión .NET: [net8.0 / net6.0 / net9.0]
- Entidad principal: [NOMBRE_ENTIDAD] (ej: Producto, Usuario, Cliente)

**Tareas a realizar:**
1. Crear la estructura completa de carpetas y proyectos
2. Configurar todas las referencias entre capas
3. Instalar todos los paquetes NuGet necesarios
4. Implementar los templates de código para la entidad especificada
5. Configurar JWT, Docker y Entity Framework
6. Crear proyectos de testing con ejemplos
7. Generar las migraciones iniciales

**Entregables esperados:**
- Solución .NET completa y funcional
- Dockerfile y docker-compose configurados
- Tests unitarios e integración funcionando
- Documentación de endpoints API
- Comandos para ejecutar el proyecto

Sigue EXACTAMENTE las instrucciones del archivo adjunto, reemplazando todos los placeholders [NombreProyecto] y [NombreEntidad] con los valores especificados.
```

## 📋 **Variantes del Prompt**

### **Para proyecto específico:**
```
Implementa una arquitectura Clean Code para un sistema de gestión de inventario:
- Proyecto: "InventarioSystem" 
- Versión: .NET 8
- Entidad: "Producto"
- Incluye autenticación JWT y Docker

Usa las instrucciones del archivo AI_ARCHITECTURE_INSTRUCTIONS.md adjunto.
```

### **Para migración de proyecto existente:**
```
Tengo un proyecto existente que quiero migrar a Clean Architecture. 
Usa el archivo AI_ARCHITECTURE_INSTRUCTIONS.md para:
1. Crear la nueva estructura
2. Migrar el código existente a las capas correctas
3. Configurar testing y Docker

Mi entidad principal es: [ENTIDAD]
Proyecto actual: [DESCRIPCIÓN_PROYECTO]
```

### **Para múltiples entidades:**
```
Implementa Clean Architecture con múltiples entidades:
- Proyecto: "[NOMBRE_PROYECTO]"
- Versión: .NET 8
- Entidades: [ENTIDAD1], [ENTIDAD2], [ENTIDAD3]

Crea la estructura base siguiendo AI_ARCHITECTURE_INSTRUCTIONS.md y luego implementa CRUD completo para cada entidad con sus respectivos controllers, servicios y tests.
```

### **Para proyecto educativo:**
```
Necesito un proyecto de demostración de Clean Architecture para enseñar:
- Proyecto: "CleanArchitectureDemo"
- Entidad: "Estudiante" 
- Incluye comentarios explicativos en el código
- Documentación detallada de cada capa
- Ejemplos de testing bien documentados

Usa AI_ARCHITECTURE_INSTRUCTIONS.md como base y agrega documentación educativa.
```

## 🎯 **Consejos para usar el prompt:**

### ✅ **Hacer:**
- Ser específico con nombres de proyecto y entidades
- Especificar la versión de .NET a usar
- Mencionar explícitamente el archivo de instrucciones
- Definir qué características adicionales necesitas (JWT, Docker, etc.)

### ❌ **Evitar:**
- Prompts vagos como "crea una API"
- No especificar la entidad principal
- Omitir la referencia al archivo de instrucciones
- No definir los entregables esperados

## 📁 **Archivos requeridos:**
- `AI_ARCHITECTURE_INSTRUCTIONS.md` (instrucciones técnicas completas)
- `PROMPT_PARA_IA.md` (este archivo con los prompts)

## 🔄 **Flujo de trabajo recomendado:**
1. Definir parámetros del proyecto (nombre, entidad, versión .NET)
2. Seleccionar la variante del prompt apropiada
3. Adjuntar el archivo `AI_ARCHITECTURE_INSTRUCTIONS.md`
4. Ejecutar el prompt con la IA
5. Validar que se siguieron todas las instrucciones
6. Probar la solución generada