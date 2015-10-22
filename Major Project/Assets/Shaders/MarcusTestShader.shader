Shader "MarcusTestShader" {
   Properties {
   
	_Color1 ("Color 1", Color) = (1,1,1,1) 
    _Color2 ("Color 2", Color) = (0.5,0.5,0.5,1) 
     }
    SubShader {
        Pass {
            Material {
                Diffuse [_Color1]
                Ambient [_Color2]
            }
           LIGHTING on
        }
    }
   
   
   
      
}