shader_type spatial;
render_mode blend_mix, depth_draw_opaque, cull_back, diffuse_lambert, specular_schlick_ggx;

uniform vec4 Tint : source_color = vec4(1.000000, 1.000000, 1.000000, 1.000000);
uniform vec2 tilling = vec2(0.000000, 0.000000);
uniform vec2 Offset = vec2(0.000000, 0.000000);
uniform sampler2D tex_frg_17;
uniform float Smoothness = 0.60000002384186;
uniform sampler2D tex_frg_23;
uniform sampler2D tex_frg_26;



void fragment() {
// ColorParameter:19
	vec4 n_out19p0 = Tint;


// Vector2Parameter:14
	vec2 n_out14p0 = tilling;


// UVFunc:15
	vec2 n_in15p2 = vec2(0.00000, 0.00000);
	vec2 n_out15p0 = (UV - n_in15p2) * n_out14p0 + n_in15p2;


// Vector2Parameter:16
	vec2 n_out16p0 = Offset;


// UVFunc:18
	vec2 n_in18p1 = vec2(1.00000, 1.00000);
	vec2 n_out18p0 = n_out16p0 * n_in18p1 + n_out15p0;


// Texture2D:17
	vec4 n_out17p0 = texture(tex_frg_17, n_out18p0);


// VectorOp:4
	vec4 n_out4p0 = n_out19p0 * n_out17p0;


// VectorDecompose:21
	float n_out21p0 = n_out4p0.x;
	float n_out21p1 = n_out4p0.y;
	float n_out21p2 = n_out4p0.z;
	float n_out21p3 = n_out4p0.w;


// FloatParameter:24
	float n_out24p0 = Smoothness;


// SmoothStep:25
	float n_in25p1 = 1.00000;
	float n_in25p2 = 0.50000;
	float n_out25p0 = smoothstep(n_out24p0, n_in25p1, n_in25p2);


// Texture2D:23
	vec4 n_out23p0 = textureLod(tex_frg_23, n_out18p0, n_out25p0);


// Texture2D:26
	vec4 n_out26p0 = texture(tex_frg_26, n_out18p0);


// Output:0
	ALBEDO = vec3(n_out4p0.xyz);
	ALPHA = n_out21p3;
	SPECULAR = n_out23p0.x;
	NORMAL = vec3(n_out26p0.xyz);


}

