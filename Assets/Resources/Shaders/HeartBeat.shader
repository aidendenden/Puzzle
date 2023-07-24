Shader "Custom/HeartBeat"
{
	Properties
	{
		_bkColor("Background Color", Color) = (0.5,0.5,0,1)
		_lColor("Line Color", Color) = (1,0.2,0.5,1)
		_lWidth("Line Width", float) = 8.0
		_interval("Light Interval Time", float) = 1.0
		_speed("Light Speed", float) = 1500.0
		_tail("Tail Length", float) = 500.0
    }

	SubShader{
		
	Pass {
			CGPROGRAM

			#pragma vertex vert
			#pragma fragment frag

			#include "UnityCG.cginc"

			#define PI 3.14159

			struct v2f
			{
				float4 pos : SV_POSITION;
				float4 scrPos : TEXCOORD0;
			};

			fixed4 _bkColor;
			fixed4 _lColor;
			float _lWidth;
			float _interval;
			float _speed;
			float _tail;

			// take the screen coordinates, return the 0~1 alpha according to the heartbeat function
			float heartbeatFunc(float2 pos, float width) 
			{
				float x = pos.x / _ScreenParams.x * 2 - 1;// xcoord~[0-_ScreenParams.x] => x~[-1, 1]
				// sin curve
				float s1 = sin(x * 20.0) / 5.0;
				// mask
				fixed b1 = -0.25, b2 = 0.25;
				float k = 10.0, a = -500.0, mask;
				if (0)
				{// hard mask
					mask = abs(k * (b1 - b2) / 2.0) - abs(k * (x - (b1 + b2) / 2.0));
					mask = clamp(mask, 0.0, 1.0);
				}
				else
				{// smooth mask
					mask = a * pow(abs(x - (b1 + b2) / 2.0) - (b2 - b1) / 2.0, 3.0);
					mask = clamp(mask, 0.0, 1.0);
				}
				
				float targetY = (s1 * mask + 0.5) * _ScreenParams.y;
				return 1.0 - smoothstep(0.0, width, abs(pos.y - targetY));
			}

			// take the screen coordinates, return the 0~1 alpha according to the light function
			float lightFunc(float2 pos, float interval, float speed, float tail) 
			{
				float tt = _Time[1] % interval;
				float highlightX = tt * speed;// the leftest highlightX
				float segment = interval * speed;// distance between two hightlightXs
				float delta;// pixel distance from the first hightlightX on the right, delta~[0,segment]
				if (pos.x <= highlightX) 
				{
					delta = highlightX - pos.x;
				}
				else 
				{
					delta = segment - (pos.x - highlightX) % segment;
				}
				float light = delta < tail ? 0.00001 * pow(tail - delta, 2.0) : 0.0;
				return light;
			}

			// take the screen coordinates, return the 0~1 alpha according to the pound function
			float poundFunc(float2 pos, float interval, float speed) 
			{
				float tt = (_Time[1] - _ScreenParams.x / 2.0 / speed) % interval;
				float percent = tt / 0.8f;//pound lasts 0.8 seconds
				float2 vec = pos / _ScreenParams.xy - 0.5;
				if (percent < 0.1) 
				{
					percent *= 10.0;
				}
				else if (percent < 1.0) 
				{
					percent = -1.0 / 0.9 * (percent - 1.0);
				}
				else 
				{
					percent = 0.0;
				}
				return percent * length(vec) * 0.2;
			}

			// take the screen coordinates, return the 0~1 alpha according to the scanline function
			float scanlineFunc(float2 pos) 
			{
				float scanline = sin(pos.y * 0.8 + _Time[1]) + sin((pos.y / _ScreenParams.y) * 12.0 + _Time[1]) * 0.3;
				return scanline * 0.06;
			}

			v2f vert(appdata_full v) 
			{
				v2f o;
				o.pos = UnityObjectToClipPos(v.vertex);
				o.scrPos = ComputeScreenPos(o.pos);

				return o;
			}

			fixed4 frag(v2f i) : SV_Target
			{
				float2 scr_coords = (i.scrPos.xy / i.scrPos.w) * _ScreenParams.xy;//片元的屏幕坐标
				
				float heartbeat = heartbeatFunc(scr_coords, _lWidth);
				float light = lightFunc(scr_coords, _interval, _speed, _tail);
				float pound = poundFunc(scr_coords, _interval, _speed);
				float scanline = scanlineFunc(scr_coords);

				float alpha = heartbeat * light + pound + scanline;
				fixed4 fragColor = lerp(_bkColor, _lColor, alpha);
				return fragColor;
			}
			ENDCG
		}
	}
}
