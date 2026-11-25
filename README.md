### El juego va de tu tienes un personaje que mueves con A, D y lanza tostadas, cada final de ronda te aparece una tienda donde te pueden tocar cartas que son torres que puedes poner para que disparen, se quitan a final de ronda. Se colocan seleccionando la carta y con el clic
## Extras:
- Hay muchos gatitos
- Todo el arte ha sido creado por nosotros
    - Lo puedes encontrar en la carpeta de Assets/Art -> Lo que hay aqui
- Uso de tileset
- Animaciones
    - Animaciones aplicadas todas desde el editor de unity
    - Materiales cambiados dentro de las animaciones con el animator
- Particulas con VFX Graph
    - Diferentes usos de particulas en colocacion de las torres, proyectiles, etc
    - Hay unas custom que utiliza particulas que orbitan alrededor de la esfera (ver /Vfx/BlueVFX, RedVFX y PurpleVFX)
- Efectos de estado
    - Dentro de MouseBehaviour y CatBehaviour, si un ataque lleva un EffectType, se aplican esos efectos
    - Hay aplicados el de veneno y el de lentitud
- Los codigos de ataques los pueden usar tanto los gatos como los ratones, es facil de reutilizar(Code/Attack)
- Creación de cartas con ScriptableObjects
    - Se pueden crear dos tipos de cartas que son las que son torres y las que son un objeto de usar y tirar(Carpeta Card)
    - Se puede indicar cuantas casillas ocupa una torre facilmente, haciendo que se puedan crear torres muy variadas, que puedan llegar a ocupar lo que quieras
- El juego es completamente adaptable a movil(Se puede comprobar con Game/Simulator)
    - Las UIs tienen anchors adaptados para que se pueda ver correctamente en las dos versiones
- Prefabs de Mouses y Cats (carpeta de Prefabs)
    - Son muy customizables, se pueden crear facilmente duplicando el prefab base
    - La interfaz de Idamageable se aplica en estos para que el recibir daño sea mucho mas facil
    - Cada una de las cartas es muy diferente entre ellas, no cambia solo el que tiene mas vida, dispara mas rapido o hace mas daño, cada una hemos intentado experimentar en añadir algo nuevo y diferente
    - Se ha intentado balancear lo maximo posible del juego(nos hemos dado cuenta que es mas complicado de lo que se esperaba xd)
    - Todo tiene una ia que la declara el ataque que tiene, todos detecta si uno de sus enemigos esta en su area de ataque, y si lo esta, hacen su ataque declarado (abstract class Attack en Code/Attack)
    - Un GRAN PATHFINDING que es ir hacia abajo a la velocidad declarada :)
- Menu Base (Scene/MainMenu)
    - Se ha hecho para que el exit se pueda usar incluso dentro de unity
    - New game te borra los datos guardados
    - Load game mantiene los datos de la ultima partida jugada
- Sistema de tiles (Code/Tile)
    - Hay un ContextMenu que hace que se detecte todas las tiles alrededor de la selecionada, automatizando el proceso de ver las tiles adyacentes
    - Cada una detecta que torre tienen puesta
 
## Uso de LLMs:
- Se ha usado sobretodo para buscar informacion de como hacer ciertas cosas concretas (el context menu, el cerrar el juego en unity
- En EnemySpawner, de la linea 75 a la 77, estaba hecho principalmente de una forma que spawneaba todas sin casi diferencia de spawn, entonces spawneaban todas muy rapidas, haciendo las rondas mucho mas dificiles, se pregunto de como se podria hacer mejor


