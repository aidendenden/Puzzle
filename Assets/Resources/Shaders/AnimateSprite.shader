Shader "LordShader/AnimateSprite" {
    Properties {
        _MainTint ("Diffuse Color", Color) = (1,1,1,1)            //颜色属性，可以在u3d inspector面板控制该变量
        _MainTex("Base (RGB)",2D) = "white" {}  //贴图
        _TexWidth("Sheet Width",float) = 0.0    //贴图宽度像素值
        _SpriteFrameNum("Sprite Frame Counts",float) = 9.0  //总帧数
        _Speed("Speed ",Range(0.01,32)) = 12    //播放速度
    }
    SubShader {
        Tags { "RenderType"="Opaque" }
        LOD 200

        CGPROGRAM
        #pragma surface surf Lambert

        fixed4 _MainTint;            //主颜色
        sampler2D _MainTex;            //主贴图
        float _TexWidth;            //贴图宽度像素值
        float _SpriteFrameNum;        //动画帧数
        float _Speed;                //播放速度
        float  _TimeValue;            //从脚本传递过来的数

        struct Input {
            float2 uv_MainTex;
        };

        void surf (Input IN, inout SurfaceOutput o) {
            float2 spriteUV = IN.uv_MainTex;
            float uAddPerFrame = 1 / _SpriteFrameNum;         //每一帧U值的增量

            //获取一个0 1 2 3 循环的值
            //fmod 返回 x/y 的余数(取模)。如果 y 为 0 ，结果不可预料
            float timeVal = fmod(_Time.y * _Speed,_SpriteFrameNum);  //进行取余数操作 得到当前要显示的图片的下标
            timeVal = ceil(timeVal);

            //float timeVal = _TimeValue;        //_TimeValue直接通过脚本传递 material.SetFloat("_TimeValue",timeVal);
            float xValue = spriteUV.x;            //UV坐标中的X坐标（0到9）
            xValue *= uAddPerFrame;                //把UV值指定到第一张小图的范围 注意

            xValue += timeVal * uAddPerFrame; //每次执行把图片切下一张小图，累加u的增量值
            spriteUV = float2(xValue,spriteUV.y);
            fixed4 c = tex2D (_MainTex, spriteUV) * _MainTint;
            o.Albedo = c.rgb * _MainTint;
            o.Alpha = c.a;
        }
        ENDCG
    }
    FallBack "Diffuse"
}