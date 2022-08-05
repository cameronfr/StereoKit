//--name = sk/blit
//--source = white

//--blit_to = = 0,0,1,1

float4 blit_to;

Texture2D    source   : register(t0);
SamplerState source_s : register(s0);

cbuffer StereoKitBuffer : register(b1) {
	float4x4 sk_view       [2];
	float4x4 sk_proj       [2];
	float4x4 sk_proj_inv   [2];
	float4x4 sk_viewproj   [2];
	float4   sk_lighting_sh[9];
	float4   sk_camera_pos [2];
	float4   sk_camera_dir [2];
	float4   sk_fingertip  [2];
	float4   sk_cubemap_i;
	float    sk_time;
	uint     sk_view_count;
};

cbuffer TransformBuffer : register(b2) {
	float sk_width;
	float sk_height;
	float sk_pixel_width;
	float sk_pixel_height;
};

struct vsIn {
	float4 pos  : SV_Position;
	float3 norm : NORMAL0;
	float2 uv   : TEXCOORD0;
	float4 col  : COLOR0;
};
struct psIn {
	float4 pos : SV_POSITION;
	float2 uv  : TEXCOORD0;
};

psIn vs(vsIn input) {
	psIn o;
	o.pos.zw = input.pos.zw;
	o.pos.xy = float2(0.5, 0.5) + input.pos.xy * 0.5;
	o.pos.xy = blit_to.xy + (o.pos.xy * blit_to.zw);
	o.pos.xy = (o.pos.xy - float2(0.5, 0.5)) * 2;
	o.pos.y  = -o.pos.y;
	o.uv     = float2(input.uv.x, 1-input.uv.y);
	return o;
}

float4 ps(psIn input) : SV_TARGET{
	return source.Sample(source_s, input.uv);
}