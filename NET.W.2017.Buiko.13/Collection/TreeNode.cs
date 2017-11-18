namespace Collection
{
    internal class TreeNode<T>
    {
        internal TreeNode<T> Left;

        internal TreeNode<T> Rigth;

        internal T Data;

        internal TreeNode(TreeNode<T> left, TreeNode<T> rigth, T data)
        {
            Left = left;
            Rigth = rigth;
            Data = data;
        }
    }
}
