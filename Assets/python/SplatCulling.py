# This is a Splat Culling Script we created to test different Gaussian Splat densities and see their effect on performance
# Added to the repository for documentation
# To run this, you require the imports below
# Tested with Python 3.12


import argparse
import numpy as np

from plyfile import PlyData, PlyElement
from pathlib import Path

def sort_Splats(v, mode:str):
    n = len(v)
    names = set(v.dtype.names or [])
    if mode == "random":
        rand = np.random.default_rng(42)
        return rand.permutation(n)

    if mode == "opacity":
        if "opacity" not in names:
            raise ValueError("You selected mode=Opacity but there is no 'opacity' property in the provided PLY vertex data")
        return np.argsort(v["opacity"])[::-1]

    if mode == "opacity_and_scale":
        required = {"opacity", "scale_0", "scale_1", "scale_2"}
        if not required.issubset(names):
            raise ValueError("You selected mode=opacity_and_scale, but the provided PLY vertex data does not contain all required values")
        sort_score = v["opacity"] * (v["scale_0"]+ v["scale_1"] + v["scale_2"])
        return np.argsort(sort_score)[::-1]
    raise ValueError(f"Unknown mode: {mode}")




if __name__ == "__main__":
    parser = argparse.ArgumentParser()
    parser.add_argument("filename", help="the name of the file you would like to cull",type=Path)
    parser.add_argument("steps", help="The culling percentage steps",type=str)
    parser.add_argument("mode", choices=["random", "opacity", "opacity_and_scale"], default="random", nargs="?")
    parser.add_argument("output", type=Path)

    args = parser.parse_args()
    args.output.mkdir(parents=True, exist_ok=True)
    step_args = args.steps.split(",")
    steps = [int(x) for x in step_args]
    mode = args.mode
    ply = PlyData.read(str(args.filename))
    data = ply["vertex"].data
    order = sort_Splats(data, mode)
    for step in steps:
        if step > len(data):
            continue
        keep = order[:step]
        culled = data[keep]

        out_vertex_element = PlyElement.describe(culled, "vertex")
        out = PlyData([out_vertex_element],text=ply.text,byte_order=ply.byte_order)

        out_path = args.output / f"{args.filename.stem}_{step}.ply"
        out.write(str(out_path))




