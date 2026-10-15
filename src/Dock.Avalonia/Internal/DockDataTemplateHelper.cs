// Copyright (c) Wiesław Šoltés. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for details.

using System;
using System.Collections.Generic;
using Avalonia.Controls;
using Avalonia.Controls.Templates;
using Dock.Avalonia.Controls;
using Dock.Controls.ProportionalStackPanel;
using Dock.Model.Controls;

namespace Dock.Avalonia.Internal;

/// <summary>
/// Helper class for creating default DataTemplates for dock controls.
/// </summary>
internal static class DockDataTemplateHelper
{
    /// <summary>
    /// Creates a collection of default DataTemplates for all dock control types.
    /// </summary>
    /// <returns>A collection of DataTemplates for dock controls.</returns>
    public static IEnumerable<IDataTemplate> CreateDefaultDataTemplates()
    {
        yield return CreateDataTemplate<IDocumentContent>(() => new DocumentContentControl());
        yield return CreateDataTemplate<IToolContent>(() => new ToolContentControl());
        yield return new FuncDataTemplate<IProportionalDockSplitter>((data, _) =>
        {
            var splitter = new ProportionalStackPanelSplitter();
            if (data is not null)
            {
                splitter.IsResizingEnabled = data.CanResize;
                splitter.PreviewResize = data.ResizePreview;
            }
            return splitter;
        });
        yield return new FuncDataTemplate<IGridDockSplitter>((data, _) =>
        {
            var splitter = new GridSplitter();
            if (data is not null)
            {
                splitter.ResizeDirection = data.ResizeDirection switch
                {
                    Dock.Model.Core.GridResizeDirection.Columns => GridResizeDirection.Columns,
                    Dock.Model.Core.GridResizeDirection.Rows => GridResizeDirection.Rows,
                    _ => GridResizeDirection.Auto
                };
            }
            return splitter;
        });
        yield return CreateDataTemplate<IDocumentDock>(() => new DocumentDockControl());
        yield return CreateDataTemplate<IToolDock>(() => new ToolDockControl());
        yield return CreateDataTemplate<ISplitViewDock>(() => new SplitViewDockControl());
        yield return CreateDataTemplate<IProportionalDock>(() => new ProportionalDockControl());
        yield return CreateDataTemplate<IStackDock>(() => new StackDockControl());
        yield return CreateDataTemplate<IGridDock>(() => new GridDockControl());
        yield return CreateDataTemplate<IWrapDock>(() => new WrapDockControl());
        yield return CreateDataTemplate<IUniformGridDock>(() => new UniformGridDockControl());
        yield return CreateDataTemplate<IDockDock>(() => new DockDockControl());
        yield return CreateDataTemplate<IRootDock>(() => new RootDockControl());
    }

    /// <summary>
    /// Creates a generic DataTemplate for the specified type.
    /// </summary>
    /// <typeparam name="T">The data type for the template.</typeparam>
    /// <param name="factory">Factory function to create the control.</param>
    /// <returns>A DataTemplate for the specified type.</returns>
    private static FuncDataTemplate<T> CreateDataTemplate<T>(Func<Control> factory) where T : class
    {
        return new FuncDataTemplate<T>((_, _) => factory());
    }
}
