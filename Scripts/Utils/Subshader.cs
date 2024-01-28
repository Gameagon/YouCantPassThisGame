using Godot;
using System;

public partial class VisualShaderNodeSubshader : VisualShaderNodeCustom
{
	public override string _GetName()
	{
		return "Subshader";
	}

	public override string _GetCategory()
	{
		return "Special";
	}

	public override string _GetDescription()
	{
		return "Shader para agroupar nodos";
	}

    public override PortType _GetReturnIconType()
	{
		return VisualShaderNode.PortType.Scalar;
	}

    public override int _GetInputPortCount()
	{
		return 0;
	}

    public override string _GetInputPortName(int port)
	{
		return "";
	}

	public override PortType _GetInputPortType(int port)
	{
		return 0;
	}

    public override int _GetOutputPortCount()
	{
		return 1;
	}


    public override string _GetCode(Godot.Collections.Array<string> inputVars, Godot.Collections.Array<string> outputVars, Shader.Mode mode, VisualShader.Type type)
	{
		return "";

	}
}
