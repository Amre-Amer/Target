using System;
using System.Linq;
using Unity.InferenceEngine;
using UnityEngine;

public class InferMgr : MonoBehaviour
{
    public GlobalsMgr g;

    public void StartInference()
    {
        Model runtimeModel = ModelLoader.Load(g.model);
        g.worker = new Worker(runtimeModel, BackendType.GPUCompute);

        // 2. Allocate a 1x3x60x60 (NCHW) input tensor for an RGB image
        // Change channels to 1 if your model expects grayscale: new TensorShape(1, 1, 60, 60)
        g.inputTensor = new Tensor<float>(new TensorShape(1, 3, g.width, g.height));
    }

    public void Inference()
    {
        g.toolsMgr.GrabImage();
        TextureConverter.ToTensor(g.textureGrabbed, g.inputTensor);
        g.worker.Schedule(g.inputTensor);
        Tensor<float> outputTensor = g.worker.PeekOutput() as Tensor<float>;

        float[] output = outputTensor.ReadbackAndClone().DownloadToArray();
        g.normsOutput = g.toolsMgr.NormalizeArray(output);
        
        g.toolsMgr.SetClassificationFromInference();

        ShowTallyInference();
    }

    void ShowTallyInference()
    {
        g.toolsMgr.ClearTally();
        for (int n = 0; n < g.normsOutput.Length; n++)
        {
            float value = g.normsOutput[n];
            int v = (int)(value * g.lengthInfo);
            g.toolsMgr.SetTally(n, v);
        }
        g.toolsMgr.ShowTally();
    }

    private void OnDestroy()
    {
        // Always dispose native tensors and workers to prevent memory leaks
        g.inputTensor?.Dispose();
        g.worker?.Dispose();
    }
}
